#pragma once
#include "Test.h"
#include "CommonHeaders.h"
#include "../Components/Entity.h"
#include "../Components/Transform.h"

#include <ctime>

using namespace primal;

class EngineTest: public Test
{
public:
	bool initialize() override
	{
		srand((u32)time(nullptr));
		return true;
	}

	void run() override
	{
		do
		{
			for (u32 i = 0; i < 10000; ++i)
			{
				create_random();
				remove_random();
				_num_entities = _entities.size();
			}
			print_result();
		} while (getchar() != 'q');
	}

	void shutdown() override
	{
	}
private:
	inline void print_result()
	{
		std::cout << "Entity has been added " << _added_entities << " times." << std::endl;
		std::cout << "Entity has been removed " << _removed_entities << " times." << std::endl;
		std::cout << "Entities count: " << _num_entities << std::endl;
		std::cout << "Currnet max geneartion: " << (u32)entity::get_max_generation() << std::endl;
		std::cout << "Currnet min geneartion: " << (u32)entity::get_min_generation() << std::endl;
		std::cout << "Currnet average geneartion: " << (f64)entity::get_average_generation() << std::endl;
	}

	inline void create_random()
	{
		u32 add_num = rand() % 20;
		if (_entities.empty())
			add_num = 1000;
		for (u32 i = 0; i < add_num; ++i)
		{
			entity::entity_info entity_info;
			transform::init_info transfom_info;
			entity_info.transform_info = &transfom_info;
			entity::game_entity new_entity = entity::create(entity_info);
			_entities.emplace_back(new_entity);
			++_added_entities;
		}
	}

	inline void remove_random()
	{
		u32 remove_num = rand() % 20;
		if (_entities.size() < 1000)
			return;
		for (u32 i = 0; i < remove_num; ++i)
		{
			u32 rm_index = rand() % _entities.size();
			entity::game_entity entity_to_remove = _entities[rm_index];
			entity::remove(entity_to_remove);
			_entities.erase(_entities.begin() + rm_index);
			++_removed_entities;
		}
	}

	util::vector<entity::game_entity> _entities;
	u32 _num_entities = 0;
	u32 _added_entities = 0;
	u32 _removed_entities = 0;
};