#include "ComponentsCommon.h"
#include "Entity.h"
#include "Transform.h"
#include "Script.h"

#include "../Utilities/Utilities.h"

namespace primal::entity
{
	namespace
	{
		// Elements in 'transforms' index into the actual data of 'position', 'rotation' and 'scale'
		util::vector<transform::component> transforms;  // Length in synchronized with entity
		// Elements in 'scripts' index into the actual data of entity script.'id_mapping' in this case because we use double indexing regarding script component
		util::vector<script::component> scripts;  // Length in synchronized with entity
		// Elements in 'generations' donets the generation part of the entity id
		util::vector<id::generation_type> generations;  // Length in synchronized with entity
		
		util::deque<entity_id> free_ids;  // from old to new.'free_ids' aslo contains generation part and index part.
	}  // Anonymous namespace.Restricted to this file only, so that we can use the same name in other files without worrying about name collision.

	// 创建entity的过程包括了获得entity和其组件的索引（匿名空间中定义的变量）以及创建各个组件的数据内容（在各个组件的匿名空间中定义的变量）这两个部分
	game_entity create(const entity_info& info)
	{
		assert(info.transform_info != nullptr);
		assert(transforms.size() == generations.size());
		if (!info.transform_info)
			return game_entity();

		entity_id new_id;
		//if (generations.size() < id::internal::min_rm_num || free_ids.empty())  Fatal Error!!!Wether to fill holes depends on how many holes there are in the memeory!;
		if(free_ids.size() < id::detail::min_rm_num)  // This ensures that there are at least 'min_rm_num' number of holes in memory;
		{
			// Create at the end
			new_id = entity_id(static_cast<id::id_type>(generations.size()));  // New added id has generation of 0.So directly assgining 'new_id' a value of 'genenations.size()' is ok.
			generations.emplace_back();   // Add 0 value by defalut.Dont use genenations.resize(), which may triggers memeory reallocation;
			transforms.emplace_back();  // Add a transform component
			scripts.emplace_back();  // Add a script component
		}
		else
		{
			// Fill in the hole
			assert(!game_entity(free_ids.front()).is_alive());
			new_id = entity_id(id::new_generation(free_ids.front()));
			assert(id::index(new_id) < generations.size());
			free_ids.pop_front();  // Fatal Error!!!Well, I forgot to modify 'free_ids' somehow.Add this line of code!
			++generations[id::index(new_id)];
		}

		game_entity new_entity(new_id);
		id::id_type entity_index_part(id::index(new_id));

		assert(!transforms[entity_index_part].is_valid());  // We always create a transform component in the invalid place
		transforms[entity_index_part] = transform::create(*info.transform_info, new_entity);
		scripts[entity_index_part] = info.script_info ? script::create(*info.script_info, new_entity) : script::component();

		// entity id index index into the 'transform' array
		return new_entity;
	}

	bool is_alive(game_entity e)
	{
		return e.is_alive();
	}

	bool remove(game_entity e)
	{
		return e.remove();
	}

	id::generation_type get_max_generation()
	{
		return *util::max_element(generations.begin(), generations.end());
	}

	id::generation_type get_min_generation()
	{
		return *util::min_element(generations.begin(), generations.end());
	}

	f64 get_average_generation()
	{
		return util::accumulate(generations.begin(), generations.end(), 0.f) / generations.size();
	}
	
	bool game_entity::is_alive() const
	{
		assert(is_valid());
		assert(get_index() < generations.size());
		assert(generations[get_index()] == id::generation(get_id()));
		id::generation_type generation = get_generation();
		entity_id index = get_index();
		return (generation == generations[index]) && transforms[index].is_valid();
	}

	bool game_entity::remove()
	{
		assert(is_valid());
		id::generation_type generation = get_generation();
		entity_id index = get_index();
		assert(is_alive());
		assert(get_index() < generations.size());
		if (!is_alive())
			return false;
		free_ids.emplace_back(_id);  // add a free id
		transforms[get_index()].remove();
		scripts[get_index()].remove();

		//transforms[get_index()] = transform::component();  // Invalidate transform id.This is the behaviour of entity, not transform component!
		//scripts[get_index()] = script::component();  // Invalidate script id.This is the behaviour of entity, not script component!

		return true;
	}


	transform::component game_entity::get_transform() const
	{
		assert(is_valid());
		assert(is_alive());
		assert(get_index() < generations.size());
		assert(transforms.size() == generations.size());
		return transforms[get_index()];
	}

	script::component game_entity::get_script() const
	{
		assert(is_valid());
		assert(is_alive());
		assert(get_index() < generations.size());
		assert(scripts.size() == generations.size());
		return scripts[get_index()];
	}
}