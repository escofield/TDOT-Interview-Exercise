/*
 * TDOT Roadway Cameras
 *
 * TDOT Roadway Cameras.
 *
 * OpenAPI spec version: 1.0.0
 * Contact: escofieldpublic@gmail.com
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Models
{

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class CameraSearch :  IEquatable<CameraSearch>
    {
        /// Constructor
        public CameraSearch() {;}
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CameraSearch" /> class.
        /// </summary>
        /// <param name="Route">Route.</param>
        /// <param name="Jurisdiction">Jurisdiction.</param>
        public CameraSearch(string Route = default(string), string Jurisdiction = default(string))
        {
            this.Route = Route;
            this.Jurisdiction = Jurisdiction;
            
        }

        /// <summary>
        /// Gets or Sets Route
        /// </summary>
        [DataMember(Name="route")]
        public string Route { get; set; }
        /// <summary>
        /// Gets or Sets Jurisdiction
        /// </summary>
        [DataMember(Name="jurisdiction")]
        public string Jurisdiction { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CameraSearch {\n");
            sb.Append("  Route: ").Append(Route).Append("\n");
            sb.Append("  Jurisdiction: ").Append(Jurisdiction).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((CameraSearch)obj);
        }

        /// <summary>
        /// Returns true if CameraSearch instances are equal
        /// </summary>
        /// <param name="other">Instance of CameraSearch to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CameraSearch other)
        {

            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    this.Route == other.Route ||
                    this.Route != null &&
                    this.Route.Equals(other.Route)
                ) && 
                (
                    this.Jurisdiction == other.Jurisdiction ||
                    this.Jurisdiction != null &&
                    this.Jurisdiction.Equals(other.Jurisdiction)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            // credit: http://stackoverflow.com/a/263416/677735
            unchecked // Overflow is fine, just wrap
            {
                int hash = 41;
                // Suitable nullity checks etc, of course :)
                    if (this.Route != null)
                    hash = hash * 59 + this.Route.GetHashCode();
                    if (this.Jurisdiction != null)
                    hash = hash * 59 + this.Jurisdiction.GetHashCode();
                return hash;
            }
        }

        #region Operators
        /// Equality operator
        public static bool operator ==(CameraSearch left, CameraSearch right)
        {
            return Equals(left, right);
        }
        /// Inequality Operator
        public static bool operator !=(CameraSearch left, CameraSearch right)
        {
            return !Equals(left, right);
        }

        #endregion Operators

    }
}
