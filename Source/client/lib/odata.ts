export interface ODataCollectionResponse<T> {
  '@odata.context': string;
  '@odata.count'?: number;
  value: T[];
}

export enum SortDirection {
  none,
  asc,
  desc,
}

function ODataSort(property: string, sort: SortDirection) {
  switch (sort) {
    case SortDirection.asc:
      return `&$orderby=${property} asc`;
    case SortDirection.desc:
      return `&$orderby=${property} desc`;
    default:
      return '';
  }
}

export function queryOData(
  route: string,
  version: string,
  count: boolean,
  skip?: number,
  top?: number,
  sort?: [prop: string, direction: SortDirection][]
) {
  route = `${process.env.serverUrl}api/${route}?api-version=${version}`;

  if (count) route += '&$count=true';
  if (skip) route += '&$skip=' + skip;
  if (top) route += '&$top=' + top;

  sort?.forEach(([prop, direction]) => (route += ODataSort(prop, direction)));

  return route;
}
