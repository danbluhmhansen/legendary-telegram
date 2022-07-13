import { Dispatch, SetStateAction } from 'react';

function GetCurrentPages(items: number[], selected: number) {
  if (items.length < 6) return items;
  else if (selected < 4) return items.slice(0, 5);
  else if (selected > items.at(-4)!) return items.slice(-5);
  else return items.slice(selected - 3, selected + 2);
}

export default function Pagination({
  count,
  page,
  setPage,
  pageSize,
  setPageSize,
}: {
  count?: number;
  page: number;
  setPage: Dispatch<SetStateAction<number>>;
  pageSize: number;
  setPageSize: Dispatch<SetStateAction<number>>;
}) {
  const pageCount = count ? Math.ceil(count / pageSize) : 1;
  const pages = GetCurrentPages(Array.from(Array(pageCount + 1).keys()).slice(1), page);

  return (
    <nav className="pagination" role="navigation">
      <button className="pagination-previous" disabled={page === 1} onClick={() => setPage(page - 1)}>
        Prev
      </button>
      <button className="pagination-next" disabled={page === pages.at(-1)} onClick={() => setPage(page + 1)}>
        Next
      </button>
      <ul className="pagination-list">
        {pages.map((p) => (
          <li key={p}>
            <a className={'pagination-link' + (page === p ? ' is-current' : '')} onClick={() => setPage(p)}>
              {p}
            </a>
          </li>
        ))}
      </ul>
      <div className="select">
        <select value={pageSize} onChange={(event) => setPageSize(+event.target.value)}>
          <option>5</option>
          <option>10</option>
          <option>25</option>
        </select>
      </div>
    </nav>
  );
}
