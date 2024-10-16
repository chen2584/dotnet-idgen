// See https://aka.ms/new-console-template for more information
using IdGen;

Console.WriteLine("Hello, World!");

var basicIdGenerator = new IdGenerator(0);
var basicId = basicIdGenerator.CreateId();
Console.WriteLine($"basic create id {basicId}");
// Example id: 1296018896595189760

// Let's say we take april 1st 2020 as our epoch
var epoch = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

// Create an ID with 45 bits for timestamp, 2 for generator-id 
// and 16 for sequence
var structure = new IdStructure(45, 6, 12); // can be set as 41, 10, 12 like snowflake ids.

// Prepare options
var options = new IdGeneratorOptions(structure, new DefaultTimeSource(epoch), SequenceOverflowStrategy.SpinWait);

// Create an IdGenerator with it's generator-id set to 0, our custom epoch 
// and id-structure
var generator = new IdGenerator(0, options);

// Let's ask the id-structure how many generators we could instantiate 
// in this setup (2 bits)
Console.WriteLine("Max. generators       : {0}", structure.MaxGenerators);

// Let's ask the id-structure how many sequential Id's we could generate 
// in a single ms in this setup (16 bits)
Console.WriteLine("Id's/ms per generator : {0}", structure.MaxSequenceIds);

// Let's calculate the number of Id's we could generate, per ms, should we use
// the maximum number of generators
Console.WriteLine("Id's/ms total         : {0}", structure.MaxGenerators * structure.MaxSequenceIds);


// Let's ask the id-structure configuration for how long we could generate Id's before
// we experience a 'wraparound' of the timestamp
Console.WriteLine("Wraparound interval   : {0}", structure.WraparoundInterval(generator.Options.TimeSource));

// And finally: let's ask the id-structure when this wraparound will happen
// (we'll have to tell it the generator's epoch)
Console.WriteLine("Wraparound date       : {0}", structure.WraparoundDate(generator.Options.TimeSource.Epoch, generator.Options.TimeSource).ToString("O"));

foreach (var number in Enumerable.Range(0, 100))
{
    var id = generator.CreateId();
    Console.WriteLine($"Generate Time: {number}, Generate Id: {id}");
}