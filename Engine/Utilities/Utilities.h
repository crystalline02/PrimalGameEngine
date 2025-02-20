#pragma once
#include "CommonHeaders.h"
#include <vector>
#include <deque>
#include <algorithm>
#include <numeric>

namespace primal::util
{
#define USE_STL_VECTOR 1
#define USE_STL_DEQUE 1
#define USE_STL_MAX_ELEMENT 1
#define USE_STL_MIN_ELEMENT 1
#define USE_STL_ACCUMULATE 1

#if USE_STL_VECTOR
	template<typename T>
	using vector = std::vector<T>;
#else
	template<typename T>
	struct vector final
	{
		// TO DO: Implement our own vector container
	};
#endif // USE_STL_VECTOR

#if USE_STL_DEQUE
	template<typename T>
	using deque = std::deque<T>;
#else
	template<typename T>
	struct deque final
	{
		// TO DO: Implement our own deque container
	};
#endif

	template<typename _FwdIt>
	_FwdIt max_element(_FwdIt _first, _FwdIt _last)
	{
#if USE_STL_MAX_ELEMENT
		return std::max_element(_first, _last);
#else
	// TO DO: Implement our own max_element function
#endif
	}

	template<typename _FwdIt, typename _Pr>
	_FwdIt max_element(_FwdIt _first, _FwdIt _last, _Pr pr)
	{
#if USE_STL_MAX_ELEMENT
		return std::max_element(_first, _last, pr);
#else
		// TO DO: Implement our own max_element function
#endif
	}

	template<typename _Init,  typename _Ty>
	_Ty accumulate(_Init _first, _Init _last, _Ty _val)
	{
#if USE_STL_ACCUMULATE
		return std::accumulate(_first, _last, _val);
#else
		// TO DO: Implement our own accumulate function;
#endif
	}

	template<typename _FwdIt>
	_FwdIt min_element(_FwdIt _first, _FwdIt _last)
	{
#if USE_STL_MIN_ELEMENT
		return std::min_element(_first, _last);
#else
		// TO DO: Implement our own min_element function
#endif
	}

	template<typename _FwdIt, typename _Pr>
	_FwdIt min_element(_FwdIt _first, _FwdIt _last, _Pr pr)
	{
#if USE_STL_MIN_ELEMENT
		return std::min_element(_first, _last, pr);
#else
		// TO DO: Implement our own min_element function
#endif
	}
}