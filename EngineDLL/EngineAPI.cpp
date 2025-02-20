#include "CommonHeaders.h"
#include "Id.h"

#include "../Components/Entity.h"
#include "../Components/Transform.h"

#pragma comment(lib, "Engine.lib")

using namespace primal;

#define EDITOR_INTERFACE extern "C" _declspec(dllexport)

//C# code of 'transform_descriptor':
//[StructLayout(LayoutKind.Sequential)]
//public class TransformDescriptor
//{
//		public Vector3 Position;  // 16B aligned, has data in the first 12B
//		public Vector3 Rotation;  // 16B aligned, has data in the first 12B
//		public Vector3 Scale;  // 16B aligned, has data in the first 12B
//}
struct transform_descriptor
{
	f32 position[3];
	f32 rotation[3];  // Euler
	f32 scale[3];

	transform::init_info to_init_info() const
	{
		transform::init_info init_info;
		memcpy(init_info.position, position, _countof(position) * sizeof(position[0]));
		memcpy(init_info.scale, scale, _countof(scale) * sizeof(scale[0]));

		const DirectX::XMFLOAT3A xm_rotation_euler(rotation);
		DirectX::XMVECTOR quaternion_vector = DirectX::XMQuaternionRotationRollPitchYawFromVector(DirectX::XMLoadFloat3A(&xm_rotation_euler));
		DirectX::XMFLOAT4A quaternion_float4a;
		DirectX::XMStoreFloat4(&quaternion_float4a, quaternion_vector);
		memcpy(init_info.rotation, &quaternion_float4a.x, _countof(rotation) * sizeof(rotation[0]));
		return init_info;
	}
};

//C# code of game_entity_descriptor:
//[StructLayout(LayoutKind.Sequential)]
//public class GameEntityDescriptor
//{
//		public TransformDescriptor transform = new TransformDescriptor();
//}
struct game_entity_descriptor
{
	transform_descriptor transform;

	// Do not get entity info from member funciton.It's polluted!
	//game_entity::entity_info to_entity_info() const
	//{
	//	// This code caused fatal eror!Do not use this!
	//	//game_entity::entity_info entity_info;
	//	//transform::init_info transform_info = transform.to_init_info();
	//	//entity_info.transform_info = &transform_info;
	//	///*注意这里，返回后，函数的栈需要被清理弹出，由于变量transform_info在栈内存，所以它需要被清理，
	//	//这时候entity_info.transform_info的指针所指向的东西就没有了！*/
	//	//return entity_info;
	//}
};

// return: 4B, parameter: pointer of 8B
EDITOR_INTERFACE id::id_type CreateGameEntity(const game_entity_descriptor* descriptor)
{
	assert(descriptor != nullptr);
	transform::init_info transform_info = descriptor->transform.to_init_info();
	game_entity::entity_info entiy_info
	{
		&transform_info  // Very important:transform_info will be cleared after this function is called.Which makes entiy_info polluted.However, as entiy_info is not returned it doesnt matter.
	};
	return game_entity::create_entity(entiy_info).get_id();
}

EDITOR_INTERFACE bool RemoveGameEntity(game_entity::entity_id id)
{
	return game_entity::remove_entity(game_entity::entity(id));
}

