#pragma once
#include <typeinfo>

using u8 = uint8_t;
using u16 = uint16_t;
using u32 = uint32_t;
using u64 = uint64_t;

using s8 = int8_t;
using s16 = int16_t;
using s32 = int32_t;
using s64 = int64_t;

using f32 = float;
using f64 = double;

constexpr u8 u8_invalied_id = 0xffui8;
constexpr u16 u16_invalied_id = 0xffffui16;
constexpr u32 u32_invalied_id = 0xffff'ffffui32;
constexpr u64 u64_invalied_id = 0xffff'ffff'ffff'ffffui64;