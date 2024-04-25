public abstract class AbstractThing
{
    public abstract (double, double) GetWeightAndArea();
    
    public Guid Id;
    
    public double Weight { get; set; }
    
    public double Width { get; set; }
    
    public double Height { get; set; }
    
    public double Length { get; set; }
}