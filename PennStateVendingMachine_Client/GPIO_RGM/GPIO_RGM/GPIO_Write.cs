using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPIO_RGM
{
    /// <summary>
    /// An input pin instance that stores a unique ID and the most recent & previously read states.
    /// </summary>
    /// <typeparam name="T">Pin ID type</typeparam>
    struct OutputPin<T>
    {
        /// <summary>Input signal identifier</summary>
        public T id;
        /// <summary>Actual GPIO pin interface</summary>
        GPIOController pinInterface;

        /// <summary>
        /// Sets up an output pin.
        /// </summary>
        /// <param name="pin">GPIO pin</param>
        /// <param name="id">Unique signal identifier</param>
        public OutputPin(GPIOController.Pin pin, T id, GPIOController.State initialState)
        {
            this.id = id;                                   // Set ID
            this.pinInterface = new GPIOController(pin, GPIOController.Direction.Out, initialState); // Set up for writing
        }

        /// <summary>
        /// Sets pin state to high
        /// </summary>
        public void SetHigh()
        {
            this.pinInterface.WriteState(GPIOController.State.High);
        }

        /// <summary>
        /// Sets pin state to low
        /// </summary>
        public void SetLow()
        {
            this.pinInterface.WriteState(GPIOController.State.Low);
        }

        /// <summary>
        /// Toggles pin to opposite state
        /// </summary>
        public void TogglePin()
        {

            this.pinInterface.ToggleState();
        }
    }


    class GPIO_Write<T>
    {
        /// <summary>
        /// Number of pins
        /// </summary>
        protected int pinCount;

        /// <summary>
        /// Input pins
        /// </summary>
        protected OutputPin<T>[] pins;

        //------------------------------------ Public Methods -----------------------------------//

        // None

        //---------------------------------- Protected Methods ----------------------------------//

        /// <summary>
        /// Sets up a pin writing controller for the indicated GPIO pins
        /// </summary>
        /// <param name="pinNumbers">GPIO pin IDs to control</param>
        /// <param name="pinIDs">ID values assigned to the pins; independent of pinNumbers</param>
        protected void SetupPins(GPIOController.Pin[] pinNumbers, T[] pinIDs, GPIOController.State initialState)
        {
            if (pinNumbers.Length != pinIDs.Length)
            {
                throw new Exception("The number of pins and number of pin IDs should be the same");
            }

            this.pinCount = pinNumbers.Length;              // Get # pins
            this.pins = new OutputPin<T>[this.pinCount];     // Resize pin array

            // Set up each pin
            for (int i = 0; i < this.pinCount; i++)
            {
                this.pins[i] = new OutputPin<T>(pinNumbers[i], pinIDs[i], initialState);
            }

            return;
        }

        /// <summary>
        /// Finds pin index in pins array from its id.
        /// </summary>
        /// <param name="id">Unique pin ID</param>
        /// <returns>index in list; -1 if not found</returns>
        protected int FindIndex(T id)
        {
            int index = -1;
            for (int i = 0; i < this.pinCount; i++) // Loop until found
            {
                if (id.Equals(this.pins[i].id))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        //----------------------------------- Private Methods -----------------------------------//

        // None


    } // End of class
}
