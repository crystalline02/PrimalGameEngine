using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;
using PrimalEditor;


namespace PrimalEditor.DLLWrapper
{
    internal static class EngineAPI
    {
        private const string _dll_name = "EngineDLL.dll";

        [StructLayout(LayoutKind.Sequential)]
        public class GameEntityDescriptor
        {
            public TransformDescriptor transform;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class TransformDescriptor
        {
            public Vector3 Position;
            public Vector3 Rotation;
            public Vector3 Scale;
        }

        [DllImport(dllName: _dll_name)]
        private extern static int CreateGameEntity(GameEntityDescriptor descriptor);
        public static int CreateGameEntity(Components.GameEntity entity)
        {
            //Debug.Assert(!Utilities.Id.IsValid(entity.EntityId));
            Components.Transform? transform = entity.GetComponent<Components.Transform>();
            if (transform == null)
            {
                return Utilities.Id.INVALID_ID;
            }

            GameEntityDescriptor descriptor = new GameEntityDescriptor()
            {
                transform = new TransformDescriptor
                {
                    Position = transform.Position,
                    Rotation = transform.Position,
                    Scale = transform.Scale
                }
            };

            return CreateGameEntity(descriptor);
        }

        [DllImport (dllName: _dll_name)]
        private extern static bool RemoveGameEntity(int entity_id);
        public static bool RemoveGameEntity(PrimalEditor.Components.GameEntity entity)
        {
            Debug.Assert(Utilities.Id.IsValid(entity.EntityId));
            return RemoveGameEntity(entity.EntityId);
        }
    }
}
