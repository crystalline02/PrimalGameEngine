#include "ComponentsCommon.h"
#include "Script.h"
#include "../EngineAPI/GameEntity.h"
#include "../EngineAPI/ScriptComponent.h"
#include "../Utilities/Utilities.h"

namespace primal::script
{
	namespace
	{
		util::vector<id::id_type> id_mapping;  // Indexing into 'entity_scripts'.Length in synchronized with VALID 'scripts'
		util::vector<detail::script_ptr> entity_scripts;  //Acutal entity script data
		util::deque<script_id> free_ids;
		util::vector<id::generation_type> generations;  // Length in synchronized with 'id_mapping'
	}  // Anonymous namespace

	bool exist(script_id id)
	{
		assert(id::is_valid(id));
		id::id_type index_to_id_mapping = id::index(id);
		assert(index_to_id_mapping < generations.size() && index_to_id_mapping < id_mapping.size());
		id::id_type index_to_entity_scripts = id_mapping[index_to_id_mapping];
		return generations[index_to_id_mapping] == id::generation(id) &&
			id::is_valid(index_to_entity_scripts) &&
			entity_scripts[index_to_entity_scripts] != nullptr &&
			entity_scripts[index_to_entity_scripts]->is_valid() &&
				entity_scripts[index_to_entity_scripts]->is_alive();
	}

	bool remove(component script)
	{
		return script.remove();
	}

	component create(const init_info& info, entity::game_entity e)
	{
		if (info.creater == nullptr)
			return component();
		assert(e.is_valid());
		assert(e.is_alive());

		script_id new_id;  // indexing into 'id_mapping'

		if (free_ids.size() > id::detail::min_rm_num)
		{
			// Fill the hole
			new_id = script_id(id::new_generation(static_cast<id::id_type>(free_ids.front())));
			assert(id::index(new_id) < generations.size() && id::index(new_id) < id_mapping.size());
			assert(!exist(new_id));

			free_ids.pop_front();
			++generations[id::index(new_id)];
		}
		else
		{
			// Create at the end
			new_id = script_id(static_cast<id::id_type>(id_mapping.size()));
			id_mapping.emplace_back();
			generations.emplace_back();
		}

		id::id_type index_to_id_mapping = id::index(new_id);
		id_mapping[index_to_id_mapping] = entity_scripts.size();
		entity_scripts.emplace_back(info.creater(e));

        assert(generations[index_to_id_mapping] == id::generation(new_id));
		assert(generations.size() == id_mapping.size());
		assert(entity_scripts.size() <= id_mapping.size());
		assert(entity_scripts.back()->is_valid() && entity_scripts.back()->is_alive());
		assert(entity_scripts.back()->get_id() == e.get_id());
		return component(new_id);
	}

	bool component::remove()
	{
		script_id id = script_id(_id);
		assert(id::is_valid(id));
		if (!exist(id))
			return false;
		id::id_type index_to_id_mapping = id::index(id);
		id::id_type index_to_entity_scripts = id_mapping[index_to_id_mapping];
		id::id_type index_to_id_mapping_to_last_entity_scripts = entity_scripts.back()->get_script().get_index();
		if (entity_scripts.size() > 1)
		{
			util::erase_unorder(entity_scripts, index_to_entity_scripts);
			entity_scripts.pop_back();
		}
		else
		{
			entity_scripts.clear();
		}
		id_mapping[index_to_id_mapping_to_last_entity_scripts] = index_to_entity_scripts;
		id_mapping[index_to_id_mapping] = id::invalid_id;

		_id = script_id(id::invalid_id);
		return true;
	}

	void entity_script::begin_play()
	{
		
	}

	void entity_script::update(f32 delta_time)
	{

	}

	namespace detail
	{
		// This function works like lazy initialization.This is to ensure that when register_script is called, map is aslo initialized.
		scripts_map reg()
		{
			static scripts_map map;
			return map;
		}

		u8 register_script(size_t code, script_creater func_creater)
		{
			assert(func_creater != nullptr);
			return reg().insert(scripts_map::value_type(code, func_creater)).second;
		}

	}
}

