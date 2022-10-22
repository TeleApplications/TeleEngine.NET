﻿
namespace TeleEngine.NET.Components.Vertices.DefaultModels.Models
{
    public sealed class TriangleModel : ModelFactory<TriangleModel>
    {
        public override VertexModel Model =>
            new VertexModel()
            {
                Vertices = new float[]
                {
                    
                    0.0f,0.0f,0.0f,
                    0.0f,0.0f,1.0f,
                    0.0f,1.0f,0.0f,
                    0.0f,1.0f,1.0f,
                    1.0f,0.0f,0.0f,
                    1.0f,0.0f,1.0f,
                    1.0f,1.0f,0.0f,
                    1.0f,1.0f,1.0f,

                },
                Indexes = new uint[]
                {
                    1,7,5,
1,3,7,
1,4,3,
1,2,4,
3,8,7,
3,4,8,
5,7,8,
5,8,6,
1,5,6,
1,6,2,
2,6,8,
2,8,4,
                }
            };
    }
}
