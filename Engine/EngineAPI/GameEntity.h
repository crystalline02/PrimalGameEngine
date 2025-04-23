#pragma once
#include "../Components/ComponentsCommon.h"
#include "TransformComponent.h"
#include "ScriptComponent.h"

namespace primal::entity
{
	struct game_entity
	{
	public:
		constexpr explicit game_entity(entity_id id = entity_id(id::invalid_id)) : _id(id) {}
		constexpr entity_id get_id() const { return _id; }
		constexpr id::generation_type get_generation() const { return id::generation(_id); }
		entity_id get_index() const { return entity_id(id::index(_id)); }
		transform::component get_transform() const;
		script::component get_script() const;
		bool is_valid() const { return id::is_valid(_id); }
		bool is_alive() const;
		bool remove();
	private:
		entity_id _id;
	};
}

namespace primal::script
{
	// Acutal script data class
	struct entity_script : public primal::entity::game_entity
	{
	public:
		virtual ~entity_script() = default;  // classes that have virtual member function should have virual destructor
		virtual void begin_play();  // called when game starts
		virtual void update(f32 delta_time);  // called every frame
	protected:
		constexpr explicit entity_script(entity::game_entity e) : entity::game_entity(e.get_id()) {}
	};

	namespace detail
	{
		using script_ptr = std::unique_ptr<script::entity_script>;
		using script_creater = script_ptr(*)(entity::game_entity);
		using string_hash = std::hash <std::string>;
		using scripts_map = std::unordered_map<size_t, script_creater>;
		scripts_map reg();

		// Note that we should not seperate template function definition and declaration in .h and .cpp file!
		template<class script_class>
		script_ptr create_script(entity::game_entity e)
		{
			assert(e.is_alive() && e.is_valid());
			return std::make_unique<script_class>(e);
		}
		u8 register_script(size_t code, script_creater func_creater);

	}
}


