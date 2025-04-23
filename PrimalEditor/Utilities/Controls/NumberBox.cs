using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrimalEditor.Utilities.Controls
{
    [TemplatePart(Name = "NumberTextBlock", Type = typeof(TextBlock))]
    [TemplatePart(Name = "NumberTextBox", Type = typeof(TextBox))]

    internal class NumberBox: Control
    {
        public string Value
        {
            get
            {
                return (string)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value),
            typeof(string),
            typeof(NumberBox),
            new FrameworkPropertyMetadata("0", 
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal,
                null));

        public double Multiplier
        {
            get
            {
                return (double)GetValue(MultiplierProperty);
            }
            set
            {
                SetValue(MultiplierProperty, value);
            }
        }

        private static readonly DependencyProperty MultiplierProperty = DependencyProperty.Register("Multiplier",
            typeof(double),
            typeof(NumberBox),
            new PropertyMetadata(1.0));

        private TextBlock? _numberTextBlock;
        private TextBlock? NumberTextBlock
        {
            get => _numberTextBlock;
            set
            {
                if(_numberTextBlock != null)
                {
                    _numberTextBlock.MouseLeftButtonDown -= OnTextBlockMouseLBtnDown;
                    _numberTextBlock.MouseMove -= OnTextBlockMouseMove;
                    _numberTextBlock.MouseLeftButtonUp -= OnTextBlockMouseLBtnUp;
                }
                _numberTextBlock = value;
                if(_numberTextBlock != null)
                {
                    _numberTextBlock.MouseLeftButtonDown += OnTextBlockMouseLBtnDown;
                    _numberTextBlock.MouseMove += OnTextBlockMouseMove;
                    _numberTextBlock.MouseLeftButtonUp += OnTextBlockMouseLBtnUp;
                }
            }
        }

        private void OnChangeValuePropertyBtnClick(object sender, RoutedEventArgs e)
        {
            Value = "50.26";
        }

        private bool _captured = false;
        private double _value;
        private double _mouseStartX = 0;
        private double _mouseLastX = 0;
        private double _moveFactor = 0.01;
        private double _originalMultiplier = 0;
        private bool _valueChanged = false;

        private void OnTextBlockMouseLBtnUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            Mouse.Capture(null);

            if(!_valueChanged)
            {
                NumberTextBox!.Visibility = Visibility.Visible;
                NumberTextBox.Focus();
                NumberTextBox.SelectAll();
            }
            _captured = false;
            _valueChanged = false;
            _mouseLastX = 0;
            _mouseStartX = 0;
            Multiplier = _originalMultiplier;
            _originalMultiplier = 0;
        }

        private void OnTextBlockMouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;
            if(_captured)
            {
                double curMouseX = e.GetPosition(this).X;
                if (Math.Abs(curMouseX - _mouseStartX) >= SystemParameters.MinimumHorizontalDragDistance && !_valueChanged)
                    _valueChanged = true;

                if (_valueChanged)
                {
                    if (((Keyboard.GetKeyStates(Key.LeftShift) & KeyStates.Down) > 0) || ((Keyboard.GetKeyStates(Key.RightShift) & KeyStates.Down) > 0))
                    {
                        Multiplier = _originalMultiplier * 15;
                    }
                    else if (((Keyboard.GetKeyStates(Key.LeftCtrl) & KeyStates.Down) > 0) || ((Keyboard.GetKeyStates(Key.RightCtrl) & KeyStates.Down) > 0))
                    {
                        Multiplier = _originalMultiplier * 0.1;
                    }
                    else
                        Multiplier = 1;
                    double d = (curMouseX - _mouseLastX) * _moveFactor * Multiplier;
                    _value += d;
                    Value = _value.ToString("0.#####");

                    _valueChanged = true;
                    _mouseLastX = e.GetPosition(this).X;
                }
            }
        }

        private void OnTextBlockMouseLBtnDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled= true;

            _captured = true;
            Focus();
            Mouse.Capture(sender as TextBlock);
            _mouseStartX = e.GetPosition(this).X;
            _mouseLastX = _mouseStartX;
            _originalMultiplier = Multiplier;
            double.TryParse(Value, out _value);
        }

        private TextBox? _numberTextBox;
        private TextBox? NumberTextBox
        {
            get => _numberTextBox;
            set
            {
                _numberTextBox = value;
            }
        }

        public NumberBox()
        {
            DefaultStyleKey = typeof(NumberBox);
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(NumberBox), new PropertyMetadata("0"));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            NumberTextBlock = GetTemplateChild("NumberTextBlock") as TextBlock;
            NumberTextBox = GetTemplateChild("NumberTextBox") as TextBox;
        }


    }
}
