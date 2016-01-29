# GameSys.NumberSeries

The test project from GameSys.

Calculating a single number series.

The only thing I changed from the requirement was that rather than using *(2% of y)/25/(firstNumber)* as the growth rate,
I chose to use *(2% of y)/25*, since all the numbers would have *firstnumber* in it anyway. Therefore the subsequent number
would be generated from *growthRate***(firstNumber^(index-1))*.
