#pragma once

#include "CommonHeaders.h"

namespace primal::id
{
	using id_type = typename u32;
	// ���ڵײ��һЩ���������ǲ�ϣ�����Ǳ���Ϸ����ű��������
	namespace internal
	{
		constexpr u32 generation_bit = 8;
		constexpr u32 index_bit = sizeof(id_type) * 8 - generation_bit;
		constexpr id_type index_mask = (id_type(1) << index_bit) - 1;
		constexpr id_type generation_mask = (id_type(1) << generation_bit) - 1;
		constexpr u32 min_rm_num = 1024;  // when `min_rm_num` number of entities was removed in memeory, we begin to reuse the free holes next time creating new entity;
	}

	constexpr id_type invalid_id = id_type(-1);  // all 1
	using generation_type = std::conditional_t<internal::generation_bit <= 8, u8, std::conditional_t<internal::generation_bit <= 16, u16, u32>>;
	static_assert(sizeof(generation_type) * 8 >= internal::generation_bit && sizeof(generation_type) < sizeof(id_type));

	inline constexpr bool is_valid(id_type id)
	{
		return id != invalid_id;
	}

	inline constexpr id_type index(id_type id)
	{
		assert(is_valid(id));
		id_type index = id & internal::index_mask;
		assert(index != internal::index_mask);
		return index;
	}

	inline constexpr generation_type generation(id_type id)
	{
		assert(is_valid(id));
		id_type generation = (id >> internal::index_bit) & internal::generation_mask;
		assert(generation < internal::generation_mask);  // same as assert(generation < id_type(1) << internal::generation_bit - 1);
		return static_cast<generation_type>(generation);
	}

	inline constexpr id_type new_generation(id_type id)
	{
		id_type g_next = generation(id) + 1;
		assert(g_next < internal::generation_mask);  // same as assert(generation < id_type(1) << internal::generation_bit - 1);
		return (g_next << internal::index_bit) | index(id);
	}
}

#ifdef _DEBUG
namespace primal::id::internal
{
	struct id_base
	{
	public:
		constexpr explicit id_base(id_type id = 0):_id(id) {}
		constexpr operator id_type() const { return _id; }
	private:
		primal::id::id_type _id;
	};
} 

#define DEFINE_ID_TYPE(type) \
struct type final: primal::id::internal::id_base \
{ \
public: \
	constexpr explicit type(primal::id::id_type id = 0):id_base(id) {} \
}; \

#else
#define DEFINE_ID_TYPE(type) using type = typename primal::id::id_type;
#endif // _Debug
