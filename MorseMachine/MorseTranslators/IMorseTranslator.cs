namespace MorseMachine.MorseTranslators
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TOutput">Representation of the translated morse code</typeparam>
    public interface IMorseTranslator<TOutput>
    {
        TOutput Translate(string morseCode);
    }
}