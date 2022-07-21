import { Dispatch, SetStateAction } from 'react';
import { SortDirection } from '../lib/odata';

function Cycle(sort: SortDirection) {
  switch (sort) {
    case SortDirection.none:
      return SortDirection.asc;
    case SortDirection.asc:
      return SortDirection.desc;
    case SortDirection.desc:
      return SortDirection.none;
    default:
      return SortDirection.asc;
  }
}

function SortIcon(sort: SortDirection) {
  switch (sort) {
    case SortDirection.asc:
      return (
        <span className="icon">
          <i className="ion-iconic icon-caret-up" />
        </span>
      );
    case SortDirection.desc:
      return (
        <span className="icon">
          <i className="ion-iconic icon-caret-down" />
        </span>
      );
    default:
      break;
  }
}

export default function SortTh({
  children,
  sort,
  setSort,
}: {
  children: React.ReactNode;
  sort: SortDirection;
  setSort: Dispatch<SetStateAction<SortDirection>>;
}) {
  return (
    <th onClick={() => setSort(Cycle(sort))} className="is-clickable">
      {children}{SortIcon(sort)}
    </th>
  );
}
