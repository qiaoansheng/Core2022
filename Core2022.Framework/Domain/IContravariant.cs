using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core2022.Framework.Domain
{
    //逆变  父类赋值给子类 
    public interface IContravariant<in T>
    {
        
    }

    public class Contravariant<T> : IContravariant<T>
    {
        
    }

    //协变  子类赋值给父类
    public interface ICovariant<out T>
    {

    }

    public class Covariant<T> : ICovariant<T>
    {

    }

}
