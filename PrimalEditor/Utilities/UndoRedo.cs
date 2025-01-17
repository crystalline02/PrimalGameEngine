using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimalEditor.Utilities
{
    interface IUndoRedo
    {
        public string Name { get; }

        public void Undo();
        public void Redo();
    }

    internal class UndoRedoAction : IUndoRedo
    {
        private readonly Action? _undoAction, _redoAction;

        public string Name { get; } = "Unnamed UndoRedoAction";

        public void Redo()
        {
            _redoAction?.Invoke();
        }

        public void Undo()
        {
            _undoAction?.Invoke();
        }

        public UndoRedoAction(string name)
        {
            Name = name;
        }

        public UndoRedoAction(Action? undoAction, Action? redoAction, string name = "Unamed UndoRedoAction"): this(name)
        {
            _undoAction = undoAction ?? throw new ArgumentNullException(nameof(undoAction));
            _redoAction = redoAction ?? throw new ArgumentNullException(nameof(redoAction));
        }

        public UndoRedoAction(object instance, string property, object undoValue, object redoValue, string name): 
            this(() => instance.GetType().GetProperty(property)?.SetValue(instance, undoValue),
                () => instance.GetType().GetProperty(property)?.SetValue(instance, redoValue),
                name)
        {
        }
    }

    internal class UndoRedo
    {
        private bool _enableAdd = true;
        private ObservableCollection<IUndoRedo> _undoList = new ObservableCollection<IUndoRedo>();
        private ObservableCollection<IUndoRedo> _redoList = new ObservableCollection<IUndoRedo>();

        public ReadOnlyObservableCollection<IUndoRedo> UndoList { get; }
        public ReadOnlyObservableCollection<IUndoRedo> RedoList { get; }

        public bool CanUndo() => _undoList.Any();
        public bool CanRedo() => _redoList.Any();

        public void Undo()
        {
            if (!_undoList.Any())
                return;
            var action = _undoList.Last();
            _undoList.Remove(action);
            _enableAdd = false;  // While Undoing, preventing any UndoRedoAction adding to the UndoList or RedoList
            action.Undo();
            _enableAdd = true;  // set back true

            _redoList.Insert(0, action);
        }

        public void Redo()
        {
            if (!_redoList.Any())
                return;
            var action = _redoList.First();
            _redoList.Remove(action);
            _enableAdd = false;  // While Redoing, preventing any UndoRedoAction adding to the UndoList or RedoList
            action.Redo();
            _enableAdd = true;

            _undoList.Add(action);
        }

        public void Add(IUndoRedo action)
        {
            if (!_enableAdd)
                return;
            _redoList.Clear();
            _undoList.Add(action);
        }

        public void Clear()
        {
            _undoList.Clear();
            _redoList.Clear();
        }

        public void RegisterUndoListChangeEvent(NotifyCollectionChangedEventHandler e)
        {
            _undoList.CollectionChanged += e;
        }

        public void RegisterRedoListChangeEvent(NotifyCollectionChangedEventHandler e)
        {
            _redoList.CollectionChanged += e;
        }

        public UndoRedo()
        {
            UndoList = new ReadOnlyObservableCollection<IUndoRedo>(_undoList);
            RedoList = new ReadOnlyObservableCollection<IUndoRedo>(_redoList);
        }
    }
}
