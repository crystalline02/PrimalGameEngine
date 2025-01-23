#pragma once
#include <typeinfo>

using u8 = typename uint8_t;
using u16 = typename uint16_t;
using u32 = typename uint32_t;
using u64 = typename uint64_t;

using s8 = typename int8_t;
using s16 = typename int16_t;
using s32 = typename int32_t;
using s64 = typename int64_t;

using f64 = typename float;

constexpr u8 u8_invalied_id = 0xffui8;
constexpr u16 u16_invalied_id = 0xffffui16;
constexpr u32 u32_invalied_id = 0xffff'ffffui32;
constexpr u64 u64_invalied_id = 0xffff'ffff'ffff'ffffui64;