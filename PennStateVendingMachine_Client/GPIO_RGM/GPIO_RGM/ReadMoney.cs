using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GPIO_RGM
{
    class ReadMoney : GPIO_Read<double>
    {

        public delegate void moneyInsertedEventHandler(object source, EventArgs e);
        public event moneyInsertedEventHandler moneyInserted;
        /// <summary>
        /// Index of return pin in the pin array
        /// </summary>
        int returnIndex;

        /// <summary>
        /// Sets up a monitor for money input for the specified GPIO pins
        /// </summary>
        /// <param name="moneyPins">List of GPIO pins for money being input</param>
        /// <param name="moneyAmounts">List of decimal monetary amounts corresponding to the money pins</param>
        /// <param name="returnPin">GPIO pin for the money return</param>
        public ReadMoney(GPIOController.Pin[] moneyPins, double[] moneyAmounts, GPIOController.Pin returnPin)
        {
            this.returnIndex = moneyPins.Length;

            // Combine the money pins and return pin in one array
            var allPins = new GPIOController.Pin[moneyPins.Length + 1];  // New arrays with all pins
            var allIDs = new double[moneyPins.Length + 1];

            for (int i = 0; i < moneyAmounts.Length; i++)
            {   // Move money pins into new array
                allPins[i] = moneyPins[i];
                allIDs[i] = moneyAmounts[i];
            }
            allPins[returnIndex] = returnPin;   // Set to return pin
            allIDs[returnIndex] = -1;           // Set ID to -1

            this.SetupPins(allPins, allIDs);    // Initializes pins for reading
        }

        /// <summary>
        /// Reads all pushbutton pins once and raises the appropriate events.
        /// </summary>
        public override void ReadOnce()
        {
            for (int i = 0; i < this.pinCount; i++)     // Reads all pins and updates states
            {
                this.pins[i].ReadPin();
            }

            EvaluateMoneyPins();    // Raises events for money pins
            EvaluateReturnPin();
            return;
        }

        //---------------------------------- Protected Methods ----------------------------------//

        // None

        //----------------------------------- Private Methods -----------------------------------//

        /// <summary>
        /// Raise events for money pins as needed
        /// </summary>
        private void EvaluateMoneyPins()
        {
            for (int i = 0; i < this.returnIndex; i++)  // Check each money pin's read
            {
                if (pins[i].isChanged &&                // Different state & pushed -> event
                    pins[i].state == GPIOController.State.High)
                {
                    // TODO: Raise money input event
                    //onMoneyInserted();
                    // @param amount: this.pins[i].id
                }

            }
            // Done reading
            return;
        }

        /// <summary>
        /// Raise event for return pin as needed
        /// </summary>
        private void EvaluateReturnPin()
        {
            if (pins[returnIndex].isChanged &&
                pins[returnIndex].state == GPIOController.State.High)
            {
                // TODO: raise change return event, no params
            }
        }
        public virtual void onMoneyInserted()
        {
            if (moneyInserted != null)
            {
                moneyInserted(this,EventArgs.Empty);
            }
        }

    }
}
