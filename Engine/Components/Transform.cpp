#include "Transform.h"
#include "../Utilities//Utilities.h"

namespace primal::transform
{
	namespace
	{
		util::vector<math::v3> positions;  // Length in synchronized with 'transforms'
		util::vector<math::v4> rotations;  // Length in synchronized with 'transforms'
		util::vector<math::v3> scales;  // Length in synchronized with 'transforms'
	}

	component create_transform(const init_info& info, game_entity::entity entity)
	{
		assert(entity.is_valid());

		game_entity::entity_id entity_index = entity.get_index();
		if (entity_index >= positions.size())  // Creating a new entity
		{
			assert(positions.size() == entity_index);
			positions.emplace_back();
			rotations.emplace_back();
			scales.emplace_back();
		}

		positions[entity_index] = math::v3(info.position);
		rotations[entity_index] = math::v4(info.rotation);
		scales[entity_index] = math::v3(info.scale);

		// 对于组件的id，我们保证也有generation part，并且和entity一致
		return component(transform_id(entity.get_id()));
	}

	bool remove_transform(component transform)
	{
		return transform.remove();
	}

	bool component::remove()
	{
		assert(is_valid());
		// Do nothing just like removing a entity
		//_id = transform_id(id::invalid_id);  // For rotation, position and scale data, keep as it is.We just smash the index to the data;
		return true;
	}

	math::v3 component::get_position() const
	{
		assert(is_valid());
		return positions[get_index()];
	}
	math::v4 component::get_rotation() const
	{
		assert(is_valid());
		return rotations[get_index()];
	}

	math::v3 component::get_scale() const
	{
		assert(is_valid());
		return scales[get_index()];
	}
}