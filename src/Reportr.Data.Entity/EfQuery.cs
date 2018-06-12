namespace Reportr.Data.Entity
{
    using Reportr.Data.Querying;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Reportr.Filtering;

    public abstract class EfQuery : QueryBase
    {
        public EfQuery
            (
                EfDataSource dataSource
            )

            : base(dataSource)
        {
            // TODO: this version is for anonymous types - how do we handle them?
        }

        public override QueryColumnInfo[] Columns => throw new NotImplementedException();
    }
}
