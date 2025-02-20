#include "Entity.h"
#include "Transform.h"

#include "../Utilities/Utilities.h"

#include <iostream>

namespace primal::game_entity
{
	namespace
	{
		util::vector<transform::component> transforms;  // Length in synchronized with entity
		util::vector<id::generation_type> generations;  // Length in synchronized with entity
		util::deque<entity_id> free_ids;  // from old to new.'free_ids' aslo contains generation part and index part.
	}

	// 创建entity的过程包括了获得entity和其组件的索引（匿名空间中定义的变量）以及创建各个组件的数据内容（在各个组件的匿名空间中定义的比哪里）这两个部分
	entity create_entity(const entity_info& info)
	{
		assert(info.transform_info != nullptr);
		assert(transforms.size() == generations.size());
		if (!info.transform_info)
			return entity();

		entity_id new_id;
		//if (generations.size() < id::internal::min_rm_num || free_ids.empty())  Fatal Error!!!Wether to fill holes depends on how many holes there are in the memeory!;
		if(free_ids.size() < id::internal::min_rm_num)  // This ensures that there are at least 'min_rm_num' number of holes in memory;
		{
			// create at the end
			new_id = entity_id(static_cast<id::id_type>(generations.size()));  // New added id has generation of 0.So directly assgining 'new_id' a value of 'genenations.size()' is ok.
			generations.emplace_back();   // Add 0 value by defalut.Dont use genenations.resize(), which may triggers memeory reallocation;
			transforms.emplace_back();  // Add a transform component
		}
		else
		{
			assert(!entity(free_ids.front()).is_alive());
			new_id = entity_id(id::new_generation(free_ids.front()));
			assert(id::index(new_id) < generations.size());
			free_ids.pop_front();  // Fatal Error!!!Well, I forgot to modify 'free_ids' somehow.
			++generations[id::index(new_id)];
			// transforms component is created below
		}

		entity new_entity(new_id);
		entity_id index_part(id::index(new_id));

		transforms[index_part] = transform::create_transform(*info.transform_info, new_entity);
		return new_entity;
	}

	bool is_alive(entity e)
	{
		return e.is_alive();
	}

	bool remove_entity(entity e)
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
	
	bool entity::is_alive() const
	{
		assert(is_valid());
		assert(get_index() < generations.size());
		id::generation_type generation = get_generation();
		entity_id index = get_index();
		return (generation == generations[index]) && transforms[index].is_valid();
	}

	bool entity::remove()
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
		transforms[get_index()] = transform::component();  // Invalidate transform id.This is the behaviour of entity, not transform component!
		
		return true;
	}


	transform::component entity::get_transform() const
	{
		assert(is_valid());
		assert(is_alive());
		assert(get_index() < generations.size());
		assert(transforms.size() == generations.size());
		return transforms[get_index()];
	}
}