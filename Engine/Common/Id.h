#pragma once

#include "CommonHeaders.h"

namespace primal::id
{
	using id_type = u32;
	constexpr u32 generation_bit = 8;
	constexpr u32 index_bit = sizeof(id_type) * 8 - generation_bit;
	constexpr id_type index_mask = id_type(1) << index_bit - 1;
	constexpr id_type generation_mask = id_type(1) << generation_bit - 1;
	constexpr id_type id_mask = id_type(-1);  // all 1

	using generation_type = std::conditional_t<generation_bit <= 8, u8, std::conditional_t<generation_bit <= 16, u16, u32>>;
	static_assert(sizeof(generation_type) * 8 >= generation_bit && sizeof(generation_type) < sizeof(id_type));

	inline bool is_vallied_id(id_type id)
	{
		return id != id_mask;
	}

	inline id_type index(id_type id)
	{
		return id & index_mask;
	}

	inline id_type generation(id_type id)
	{
		return (id >> index_bit) & generation_mask;
	}

	inline id_type new_generation(id_type id)
	{
		id_type g_next = generation(id) + 1;
		assert(g_next < pow(2, generation_bit) - 1);
		return (g_next << index_bit) | index(id);
	}
}

#ifdef _DEBUG
namespace primal::internal
{
	struct id_base
	{
	public:
		constexpr explicit id_base(primal::id::id_type id = primal::id::id_mask):_id(id) {}
		constexpr primal::id::id_type opeartor() const { return _id; }
	private:
		primal::id::id_type _id;
	};
} 

#define DEFINE_ID_TYPE(type) \
struct type final: primal::internal::id_base \
{ \
public: \
	type(primal::id::id_type id = primal::id::id_mask):id_base(id) {} \
}; \

#else
#define DEFINE_ID_TYPE(type) using type = typename primal::id::id_type;
#endif // _Debug
