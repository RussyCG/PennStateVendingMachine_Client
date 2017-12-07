using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPIO_RGM
{
    class GPIOController
    {
        private const string GPIO_ROOT_DIR = "/sys/class/gpio/";

        /// <summary>
        /// GPIO pins by computer-known number
        /// </summary>
        public enum Pin
        {
            GPIO2 = 2,
            GPIO3 = 3,
            GPIO4 = 4,
            GPIO7 = 7,
            GPIO8 = 8,
            GPIO9 = 9,
            GPIO10 = 10,
            GPIO11 = 11,
            GPIO14 = 12,
            GPIO15 = 15,
            GPIO17 = 17,
            GPIO18 = 18,
            GPIO22 = 22,
            GPIO23 = 23,
            GPIO24 = 24,
            GPIO25 = 25,
            GPIO27 = 27
        }

        /// <summary>
        /// GPIO Pins by physical number
        /// </summary>
        public enum PhysicalPin
        {
            GPIO3 = 2,
            GPIO5 = 3,
            GPIO7 = 4,
            GPIO11 = 17,
            GPIO13 = 27,
            GPIO15 = 22,
            GPIO19 = 10,
            GPIO21 = 9,
            GPIO23 = 11,
            GPIO29 = 5,
            GPIO31 = 6,
            GPIO33 = 13,
            GPIO35 = 19,
            GPIO37 = 26,
            GPIO8 = 14,
            GPIO10 = 15,
            GPIO12 = 18,
            GPIO16 = 23,
            GPIO18 = 24,
            GPIO22 = 25,
            GPIO24 = 8,
            GPIO26 = 7,
            GPIO32 = 12,
            GPIO36 = 16,
            GPIO38 = 20,
            GPIO40 = 21
        }

        public enum Direction
        {
            In, Out
        }

        public enum State
        {
            Low = 0, High = 1
        }

        private Pin _pinNumber;
        private Direction _direction;
        private State _state;
        private string _path;

        /// <summary>
        /// Opens a connection with the indicated pin and intializes the direction. Deaults intial state = Low.
        /// </summary>
        /// <param name="pinNumber">Pin ID</param>
        /// <param name="direction">Direction, In or Out</param>
        public GPIOController(Pin pinNumber, Direction direction)
            : this(pinNumber, direction, State.Low) { }

        /// <summary>
        /// Open a connection with the indicated pin and initialize to the indicated direction and state.
        /// </summary>
        /// <param name="pinNumber">Pin ID</param>
        /// <param name="direction">Direction, In or Out</param>
        /// <param name="initialState">State, High or Low</param>
        public GPIOController(Pin pinNumber, Direction direction, State initialState)
        {
            _pinNumber = pinNumber; // set pin number, direction, state
            _direction = direction;
            _state = initialState;

            _path = String.Format("{0}gpio{1}", GPIO_ROOT_DIR, (int)_pinNumber);    // set GPIO string

            try
            {
                ExportPin();        // Export pin for writing
                ApplySettings();    // Apply direction and state settings
            }
            catch
            {
                throw new Exception(String.Format("Unable to initialize {0}", _pinNumber));
            }
        }

        /// <summary>
        /// Set pin state.
        /// </summary>
        /// <param name="state">State to set, High or Low</param>
        public void WriteState(State state)
        {
            _state = state;     // Update private state
            this.ApplyState();  // Apply private state
            return;
        }

        /// <summary>
        /// Toggles pin state.
        /// </summary>
        public void ToggleState()
        {
            _state = (_state == State.High) ? State.Low : State.High;   // Flip private state
            this.ApplyState();                                          // Apply private state
            return;
        }


        /// <summary>
        /// Reads pin state.
        /// </summary>
        /// <returns>Read pin state and return value.</returns>
        public State ReadState()
        {
            return ReadPinValue();
        }

        /// <summary>
        /// Exports pin if not already exported
        /// </summary>
        private void ExportPin()
        {
            int number = (int)_pinNumber;   // Must conver enum first
            if (!this.IsExported())     // Export if not already exported
            {
                //Console.WriteLine("Opening GPIO pin {0}...", _pinNumber);
                File.WriteAllText(GPIO_ROOT_DIR + "export", number.ToString());
            }

        }

        /// <summary>
        /// Unexportes pin if already exported
        /// </summary>
        private void UnexportPin()
        {
            int number = (int)_pinNumber;   // Must conver enum first
            if (this.IsExported())    // Unexport only if exported
            {
                //Console.WriteLine("Closing GPIO pin {0}...", _pinNumber);
                File.WriteAllText(GPIO_ROOT_DIR + "unexport", number.ToString());
            }
        }

        /// <summary>
        /// Determine if this GPIO pin has been exported already
        /// </summary>
        /// <returns>true if it has been exported, false o/w</returns>
        private bool IsExported()
        {
            return Directory.Exists(_path);
        }

        /// <summary>
        /// Sets pin state to match local value if Direction is out.
        /// </summary>
        private void ApplyState()
        {
            int state = (int)_state;    // Convert first
            if (_direction == Direction.Out && IsExported())
            {
                File.WriteAllText(_path + "/value", state.ToString());
            }
            return;
        }

        /// <summary>
        /// Read the pin value and convert to a State.
        /// </summary>
        /// <returns>Read state</returns>
        private State ReadPinValue()
        {
            string read = File.ReadAllText(_path + "/value");
            int readVal;
            if (!Int32.TryParse(read, out readVal))
            {
                //Console.WriteLine("Unable to convert read value to integer.");
                readVal = (int)_state;
            }
            return (readVal == 0) ? State.Low : State.High;
        }

        /// <summary>
        /// Applies direction and state settings to the GPIO pin
        /// </summary>
        private void ApplySettings()
        {
            // Convert local settings to values
            string direction = (_direction == Direction.Out) ? "out" : "in";
            int state = (int)_state;

            if (IsExported())
            {
                File.WriteAllText(_path + "/direction", direction);
                if (_direction == Direction.Out)
                {
                    File.WriteAllText(_path + "/value", state.ToString());
                }
            }
            else
            {
                throw new Exception("Cannot set values for an unexported pin.");
            }
            return;
        }

        ~GPIOController()
        {
            UnexportPin();
        }
    }
}
