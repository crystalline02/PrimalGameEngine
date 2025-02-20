#pragma once
#include "CommonHeaders.h"
#include "../Utilities/MathTypes.h"

namespace primal::transform
{
	struct component final
	{
	public:
		constexpr explicit component(transform_id id = transform_id(id::invalid_id)): _id(id) {}
		bool is_valid() const { return id::is_valid(_id); }
		constexpr transform_id get_id() const { return _id; }
		constexpr transform_id get_index() const { return transform_id(id::index(_id)); }
		math::v3 get_position() const;
		math::v4 get_rotation() const;
		math::v3 get_scale() const;
		bool remove();
	private:
		transform_id  _id;
	};
}