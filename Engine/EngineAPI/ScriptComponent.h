#pragma once
#include "../Components/ComponentsCommon.h"

FORWRAD_DECLARE(entity_script, primal::script)
namespace primal::script::detail
{
	using script_ptr = std::unique_ptr<script::entity_script>;
};

namespace primal::script
{
	struct component  final
	{
	public:
		constexpr explicit component(script_id id = script_id(id::invalid_id)) : _id(id) {}
		bool is_valid() const { return id::is_valid(_id); }
		constexpr script_id get_id() const { return _id; }
		constexpr script_id get_index() const { return script_id(id::index(_id)); }
		bool remove();
	private:
		script_id _id;
	};
}

