#include "ComponentsCommon.h"
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

	component create(const init_info& info, entity::game_entity e)
	{
		assert(e.is_valid());
		// When creating a transform component.The entity is NOT alive because we judge if it is alive by the transform component.
		assert(!e.is_alive());

		entity::entity_id new_entity_id = e.get_index();
		if (new_entity_id >= positions.size())  // Creating a new entity.So add new position,rotation and scale
		{
			assert(positions.size() == new_entity_id);
			positions.emplace_back();
			rotations.emplace_back();
			scales.emplace_back();
		}

		positions[new_entity_id] = math::v3(info.position);
		rotations[new_entity_id] = math::v4(info.rotation);
		scales[new_entity_id] = math::v3(info.scale);

		// 对于实体的transform组件的id，我们保证也有generation part，并且和entity一致
		return component(transform_id(e.get_id()));
	}

	bool remove(component transform)
	{
		return transform.remove();
	}

	bool component::remove()
	{
		assert(is_valid());
		_id = transform_id(id::invalid_id);  // For rotation, position and scale data, keep as it is.We just smash the index to the data;
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