// dllmain.cpp : Defines the entry point for the DLL application.
#include <Windows.h>
#include <crtdbg.h>

#pragma comment(lib, "Engine.lib")

// �����ǵ��õ�һ����Ҫ���ӵ�DLL�ļ��ĺ���ʱ���ĺ���������
BOOL APIENTRY DllMain(HMODULE hModule,
    DWORD  ul_reason_for_call,
    LPVOID lpReserved
)
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
#ifdef _DEBUG
        _CrtSetDbgFlag(_CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF);
#endif // _DEBUG
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

