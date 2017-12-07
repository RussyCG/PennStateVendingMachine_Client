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
    struct InputPin<T>
    {
        /// <summary>Input signal identifier</summary>
        public T id;

        /// <summary>Pin state from most recent read</summary>
        public GPIOController.State state;

        /// <summary>true if pin state changed from previous state on most recent read</summary>
        public bool isChanged;

        /// <summary>Pin state from previous read</summary>
        GPIOController.State prevState;
        /// <summary>Actual GPIO pin interface</summary>
        GPIOController pinInterface;

        /// <summary>
        /// Sets up an input pin.
        /// </summary>
        /// <param name="pin">GPIO pin</param>
        /// <param name="id">Unique signal identifier</param>
        public InputPin(GPIOController.Pin pin, T id)
        {
            this.id = id;                                // Set ID
            this.state = GPIOController.State.Low;       // Default states to Low
            this.prevState = GPIOController.State.Low;
            this.isChanged = false;

            this.pinInterface = new GPIOController(pin, GPIOController.Direction.In); // Set up for reading
        }

        /// <summary>
        /// Reads pin state and updates the stored states.
        /// </summary>
        public void ReadPin()
        {
            prevState = state;          // Update previous state
            state = pinInterface.ReadState(); // Update current state
            isChanged = (prevState != state) ? true : false;    // Check if changed
            return;
        }
    }

    abstract class GPIO_Read<T>
    {
        /// <summary>
        /// Number of pins
        /// </summary>
        protected int pinCount;

        /// <summary>
        /// Input pins
        /// </summary>
        protected InputPin<T>[] pins;

        //------------------------------------ Public Methods -----------------------------------//

        /// <summary>
        /// Reads all pins once and raises appropriate events.
        /// </summary>
        public abstract void ReadOnce();

        /// <summary>
        /// Reads all pins on a continual loop.
        /// </summary>
        public void ReadLoop()
        {
            while (true)
            {
                this.ReadLoop();
            }
        }

        //---------------------------------- Protected Methods ----------------------------------//

        /// <summary>
        /// Sets up a pin reading monitor for the indicated GPIO pins
        /// </summary>
        /// <param name="pinNumbers">GPIO pin IDs to monitor</param>
        /// <param name="pinIDs">ID values assigned to the pins; independent of pinNumbers</param>
        protected void SetupPins(GPIOController.Pin[] pinNumbers, T[] pinIDs)
        {
            if (pinNumbers.Length != pinIDs.Length)
            {
                throw new Exception("The number of pins and number of pin IDs should be the same");
            }

            this.pinCount = pinNumbers.Length;              // Get # pins
            this.pins = new InputPin<T>[this.pinCount];     // Resize pin array

            // Set up each pin
            for (int i = 0; i < this.pinCount; i++)
            {
                this.pins[i] = new InputPin<T>(pinNumbers[i], pinIDs[i]);
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
