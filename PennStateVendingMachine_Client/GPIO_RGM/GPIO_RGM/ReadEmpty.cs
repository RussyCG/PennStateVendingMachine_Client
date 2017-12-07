using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPIO_RGM
{

    class ReadEmpty : GPIO_Read<int> // Inherit from GPIO_Read
    {

        
        /// <summary>
        /// Sets up an empty column monitor for the specified GPIO Pins
        /// </summary>
        /// <param name="emptyPins">List of GPIO pins to run empty sensors on</param>
        /// <param name="columnIDs">List of column IDs; index corresponds to emptyPins</param>
        public ReadEmpty(GPIOController.Pin[] emptyPins, int[] columnIDs)
        {
            this.SetupPins(emptyPins, columnIDs);   // Initializes values
        }

        //------------------------------------ Public Methods -----------------------------------//

        /// <summary>
        /// Checks if the indicated column is empty.
        /// </summary>
        /// <param name="columnID">Column ID, specified when the monitor was constructed.</param>
        /// <returns>true if column is empty, false o/w</returns>
        public bool IsEmpty(int columnID)
        {
            int index = this.FindIndex(columnID);                           // Find column ID
            GPIOController.State state = this.pins[index].state;         // Get state

            return (state == GPIOController.State.High) ? true : false;  // High = empty
        }

        /// <summary>
        /// Reads all empty sensor pins once.
        /// </summary>
        public override void ReadOnce()
        {
            for (int i = 0; i < this.pinCount; i++) // Check pin values and raise events
            {
                this.pins[i].ReadPin();
                if (this.pins[i].isChanged)  // Different state -> raise event
                {
                    // TODO: Raise empty column event!
                    // @param column: this.pins[i].id
                    // @param state:  this.pins[i].state
                    Console.WriteLine("TODO: Empty Column Event: col {0} became {1}", this.pins[i].id, this.pins[i].state);
                    
                }

            }
            // Done reading all pins
            return;
        }

        //---------------------------------- Protected Methods ----------------------------------//

        // None

        //----------------------------------- Private Methods -----------------------------------//

        // None

    } // End of class
}
