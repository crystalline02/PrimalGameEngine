#include <iostream>

#pragma comment(lib, "Engine.lib")

#define TEST_ENTITY_COMPONENT 1

#if TEST_ENTITY_COMPONENT
#include "TestEntityComponent.h"
#else
#error "At least a test should be selected."
#endif

int main()
{
#ifdef  _DEBUG
	_CrtSetDbgFlag(_CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF);
#endif //  _DEBUG

	EngineTest test;
	if (test.initialize())
	{
		test.run();
	}
	test.shutdown();
}