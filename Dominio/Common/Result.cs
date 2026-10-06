namespace Dominio.Common;

public class Result
{
    protected Result(bool isSuccess, Error error)                                                                                                                                  
    {                                                                                                                                                                              
        // Invariante: éxito sin error, fallo siempre con error                                                                                                                    
        if ((isSuccess && error != Error.none) || (!isSuccess && error == Error.none))                                                                                             
            throw new ArgumentException("Combinación inválida de éxito y error.", nameof(error));                                                                                  
                                                                                                                                                                                     
        IsSuccess = isSuccess;                                                                                                                                                     
        Error = error;                                                                                                                                                             
    } 
    
    public bool IsSuccess { get; }                                                                                                                                                 
    public bool IsFailure => !IsSuccess;                                                                                                                                           
    public Error Error { get; }


    public static Result Success() => new(true, Error.none);                                                                                                                       
    public static Result Failure(Error error) => new(false, error);                                                                                                                
                                                                                                                                                                                     
    public static implicit operator Result(Error error) => Failure(error); 
}

public sealed class Result<T> : Result                                                                                                                                             
  {                                                                                                                                                                                  
      private readonly T? _value;                                                                                                                                                    
                                                                                                                                                                                     
      private Result(T value) : base(true, Error.none) => _value = value;                                                                                                            
      private Result(Error error) : base(false, error) { }                                                                                                                           
                                                                                                                                                                                     
      // Leer Value de un fallo es un bug del llamador: obliga a chequear IsSuccess                                                                                                  
      public T Value => IsSuccess                                                                                                                                                    
          ? _value!                                                                                                                                                                  
          : throw new InvalidOperationException("No se puede leer valor de un resultado fallido.");                                                                                  
                                                                                                                                                                                     
      public static Result<T> Success(T value) => new(value);                                                                                                                        
      public static new Result<T> Failure(Error error) => new(error);                                                                                                                
                                                                                                                                                                                     
      public static implicit operator Result<T>(T value) => Success(value);                                                                                                          
      public static implicit operator Result<T>(Error error) => Failure(error);                                                                                                      
  }      