using PrimalEditor.Components;
using PrimalEditor.GameProject;
using PrimalEditor.Utilities;
using PrimalEditor.Utilities.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;

namespace PrimalEditor.Editors
{
    /// <summary>
    /// Interaction logic for TransformControl.xaml
    /// </summary>
    public partial class TransformControl : UserControl
    {
        public TransformControl()
        {
            /*我们来讨论以下TransformControl什么时候被初始化，然后调用Loaded函数。此处的控件是作为GameEntityControl中的一个ItemsControl来展示的，它的
             * ItemSouce是MSComponents，只要当MSComponents发生变化，那么其中所有的MSComponent就会重新加载。那MSComponents什么时候变化呢？
             * MSComponents作为MSEntity的一个属性出现，当MSGameEntity变化，则MSComponents变化。所以显而易见了，当我们重新选择了一些GameEntity
             * 则代码重新构建MSEntity，MSComponents重新更新了，Item全部重新更新，这时TransformControl就重新加载了*/
            InitializeComponent();
            Loaded += (s, e) =>
            {
                (DataContext as MSTransform)!.PropertyChanged += (s, e) =>
                {
                    _propertyChanged = true;
                };
            };
        }

        private Action? _undoAction = default;
        private bool _propertyChanged = false;

        private Action GetUndoAction(Func<Transform, Vector3> pickElement, Action<Transform, Vector3> setElement)
        {
            MSTransform? curMSTransform = DataContext as MSTransform;
            List<(Transform, Vector3)>? curTransformList = curMSTransform?.SelectedComponents.Select(comp => (comp, pickElement(comp))).ToList();

            return () =>
            {
                curTransformList?.ForEach((te) =>
                {
                    Transform t = te.Item1;
                    Vector3 e = te.Item2;
                    setElement(t, e);
                });
                curMSTransform?.Refresh();
            };
        }

        private Action GetRedoAction(Vector3 newValue, Action<Transform, Vector3> setElement)
        {
            MSTransform? curMSTransform = DataContext as MSTransform;
            return () =>
            {
                curMSTransform?.SelectedComponents.ForEach(comp =>
                {
                    setElement(comp, newValue);
                });
                curMSTransform?.Refresh();
            };
        }


        private void OnPositionVectorBoxMouseDown(object sender, MouseButtonEventArgs e)
        {
            _propertyChanged = false;
            VectorBox? curVectorBox = sender as VectorBox;
            MSTransform? curMSTransform = DataContext as MSTransform;
            if (curVectorBox == null || curMSTransform == null)
                return;

            _undoAction = GetUndoAction((t) => t.Position, (t, p) => t.Position = p);
        }        

        private void OnPositionVectorBoxMouseUp(object sender, MouseButtonEventArgs e)
        {
            VectorBox? curVectorBox = sender as VectorBox;
            MSTransform? curMSTransform = DataContext as MSTransform;
            if (curVectorBox == null || curMSTransform == null || !_propertyChanged)
                return;

            Vector3 newPostion = new Vector3(float.Parse(curVectorBox.X), float.Parse(curVectorBox.Y), float.Parse(curVectorBox.Z));
            Action redoAction = GetRedoAction(newPostion, (t, p) => t.Position = p);

            Project.UndoRedoManager.Add(new UndoRedoAction(_undoAction, redoAction, $"Selected game entity transform position modified"));
            _propertyChanged = false;
        }

        private void OnRotationVectorBoxMouseDown(object sender, MouseButtonEventArgs e)
        {
            _propertyChanged = false;
            VectorBox? curVectorBox = sender as VectorBox;
            MSTransform? curMSTransform = DataContext as MSTransform;
            if (curVectorBox == null || curMSTransform == null)
                return;

            _undoAction = GetUndoAction((t) => t.Rotation, (t, p) => t.Rotation = p);
        }

        private void OnRotationVectorBoxMouseUp(object sender, MouseButtonEventArgs e)
        {
            VectorBox? curVectorBox = sender as VectorBox;
            MSTransform? curMSTransform = DataContext as MSTransform;
            if (curVectorBox == null || curMSTransform == null || !_propertyChanged)
                return;

            Vector3 newRotation = new Vector3(float.Parse(curVectorBox.X), float.Parse(curVectorBox.Y), float.Parse(curVectorBox.Z));
            Action redoAction = GetRedoAction(newRotation, (t, p) => t.Rotation = p);
            Project.UndoRedoManager.Add(new UndoRedoAction(_undoAction, redoAction, $"Selected game entity transform rotation modified"));
            _propertyChanged = false;
        }

        private void OnScaleVectorBoxMouseDown(object sender, MouseButtonEventArgs e)
        {
            _propertyChanged = false;
            VectorBox? curVectorBox = sender as VectorBox;
            MSTransform? curMSTransform = DataContext as MSTransform;
            if (curVectorBox == null || curMSTransform == null)
                return;

            _undoAction = GetUndoAction((t) => t.Scale, (t, p) => t.Scale = p);
        }

        private void OnScaleVectorBoxMouseUp(object sender, MouseButtonEventArgs e)
        {
            VectorBox? curVectorBox = sender as VectorBox;
            MSTransform? curMSTransform = DataContext as MSTransform;
            if (curVectorBox == null || curMSTransform == null || !_propertyChanged)
                return;

            Vector3 newScale = new Vector3(float.Parse(curVectorBox.X), float.Parse(curVectorBox.Y), float.Parse(curVectorBox.Z));
            Action redoAction = GetRedoAction(newScale, (t, p) => t.Scale = p);
            Project.UndoRedoManager.Add(new UndoRedoAction(_undoAction, redoAction, $"Selected game entity transform scale modified"));
            _propertyChanged = false;
        }

        private void OnPositionVectorBoxLostFocus(object sender, RoutedEventArgs e)
        {
            // If Lost Focus event is triggered, mouse button down must have been triggered
            OnPositionVectorBoxMouseUp(sender, null);
        }

        private void OnRotationVectorBoxLostFocus(object sender, RoutedEventArgs e)
        {
            OnRotationVectorBoxMouseUp(sender, null);
        }

        private void OnScaleVectorBoxLostFocus(object sender, RoutedEventArgs e)
        {
            OnScaleVectorBoxMouseUp(sender, null);
        }
    }
}
