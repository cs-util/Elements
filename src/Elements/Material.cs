using Newtonsoft.Json;
using System;
using System.Collections;
using Elements.Geometry;

namespace Elements
{
    /// <summary>
    /// A material with red, green, blue, alpha, and metallic factor components.
    /// </summary>
    public partial class Material: Identifiable
    {
        /// <summary>
        /// Is the material double sided?
        /// </summary>
        public bool DoubleSided{get;}

        /// <summary>
        /// Construct a material.
        /// </summary>
        /// <param name="name"></param>
        public Material(string name): base(Guid.NewGuid(), name)
        {
            this.Color = Colors.Gray;
        }

        /// <summary>
        /// Construct a material.
        /// </summary>
        /// <param name="id">The unique identifier of the material.</param>
        /// <param name="name">The identifier of the material. Identifiers should be unique within a model.</param>
        /// <param name="color">The RGBA color of the material.</param>
        /// <param name="specularFactor">The specular component of the color. Between 0.0 and 1.0.</param>
        /// <param name="glossinessFactor">The glossiness component of the color. Between 0.0 and 1.0.</param>
        /// <param name="doubleSided">Is the material double sided?</param>
        /// <exception>Thrown when the specular or glossiness value is less than 0.0.</exception>
        /// <exception>Thrown when the specular or glossiness value is greater than 1.0.</exception>
        [JsonConstructor]
        public Material(Guid id, string name, Color color, float specularFactor = 0.1f, float glossinessFactor = 0.1f, bool doubleSided = false): base(id, name)
        {
            if(specularFactor < 0.0 || glossinessFactor < 0.0)
            {
                throw new ArgumentOutOfRangeException("The material could not be created. Specular and glossiness values must be less greater than 0.0.");
            }

            if(specularFactor > 1.0 || glossinessFactor > 1.0)
            {
                throw new ArgumentOutOfRangeException("The material could not be created. Color, specular, and glossiness values must be less than 1.0.");
            }
            
            this.Color = color;
            this.SpecularFactor = specularFactor;
            this.GlossinessFactor = glossinessFactor;
            this.DoubleSided = doubleSided;
        }

        /// <summary>
        /// Construct a material.
        /// </summary>
        /// <param name="name">The identifier of the material. Identifiers should be unique within a model.</param>
        /// <param name="color">The RGBA color of the material.</param>
        /// <param name="specularFactor">The specular component of the color. Between 0.0 and 1.0.</param>
        /// <param name="glossinessFactor">The glossiness component of the color. Between 0.0 and 1.0.</param>
        /// <param name="doubleSided">Is the material double sided?</param>
        /// <exception>Thrown when the specular or glossiness value is less than 0.0.</exception>
        /// <exception>Thrown when the specular or glossiness value is greater than 1.0.</exception>
        public Material(string name, Color color, float specularFactor = 0.1f, float glossinessFactor = 0.1f, bool doubleSided = false): 
            this(Guid.NewGuid(), name, color, specularFactor, glossinessFactor, doubleSided){}

        /// <summary>
        /// Is this material equal to the provided material?
        /// </summary>
        /// <param name="obj"></param>
        public override bool Equals(object obj)
        {
            var m = obj as Material;
            if(m == null)
            {
                return false;
            }
            return this.Color.Equals(m.Color) && this.SpecularFactor == m.SpecularFactor && this.GlossinessFactor == m.GlossinessFactor && this.Name == m.Name;
        }

        /// <summary>
        /// Get the hash code for the material.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return new ArrayList(){this.Name, this.Color, this.SpecularFactor, this.GlossinessFactor}.GetHashCode();
        }
    }
}