using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BraileML.Models;

public class Batch
{
    public DataPoint[] data;

    public Batch(DataPoint[] data)
    {
        this.data = data;
    }
}
