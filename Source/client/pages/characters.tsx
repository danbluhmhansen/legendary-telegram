import Head from 'next/head';
import { useEffect, useState } from 'react';
import Layout, { siteTitle } from '../components/layout';

interface ODataCollectionResponse<T> {
  '@odata.context': string;
  '@odata.count'?: number;
  value: T[];
}

interface Character {
  id: string;
  name: string;
}

enum SortDirection {
  none,
  asc,
  desc,
}

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

export default function Characters() {
  const [characters, setCharacters] = useState<Character[]>();
  const [count, setCount] = useState<number | undefined>();
  const [isLoading, setLoading] = useState(false);
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [showPageSizePicker, setShowPageSizePicker] = useState(false);
  const [idSort, setIdSort] = useState(SortDirection.none);
  const [nameSort, setNameSort] = useState(SortDirection.none);

  useEffect(() => {
    let uri = 'https://localhost:7000/api/characters?api-version=1.0&$count=true';

    uri += `&$skip=${(page - 1) * pageSize}`;
    uri += `&$top=${pageSize}`;
    uri += ODataSort('Id', idSort);
    uri += ODataSort('Name', nameSort);

    setLoading(true);
    fetch(uri)
      .then((res) => res.json())
      .then((data: ODataCollectionResponse<Character>) => {
        setCharacters(data.value);
        setCount(data['@odata.count']);
        setLoading(false);
      });
  }, [idSort, nameSort]);

  const pageCount = count ? Math.ceil(count / pageSize) : 1;
  const pages = Array.from(Array((pageCount) + 1).keys()).slice(1);

  return (
    <Layout>
      <Head>
        <title>{siteTitle} - Characters</title>
      </Head>
      <h1 className="title">Characters</h1>
      {isLoading ? (
        <progress className="progress is-primary" />
      ) : (
        characters && (
          <>
            <table className="table">
              <thead>
                <tr>
                  <th onClick={() => setIdSort(Cycle(idSort))} className="is-clickable">
                    Id{SortIcon(idSort)}
                  </th>
                  <th onClick={() => setNameSort(Cycle(nameSort))} className="is-clickable">
                    Name{SortIcon(nameSort)}
                  </th>
                </tr>
              </thead>
              <tbody>
                {characters.map((character) => (
                  <tr key={character.id}>
                    <td>{character.id}</td>
                    <td>{character.name}</td>
                  </tr>
                ))}
              </tbody>
            </table>
            <nav className="pagination" role="navigation">
              <a className={"pagination-previous" + (page === 1 && " is-disabled")}>Prev</a>
              <a className={"pagination-next" + (page === pages.at(-1) && " is-disabled")}>Next</a>
              <ul className="pagination-list">
                {pages.map(p => (
                  <li key={p}><a className={"pagination-link" + (page === p && " is-current")}>{p}</a></li>
                ))}
              </ul>
              <div className={"dropdown" + (showPageSizePicker && " is-active")}>
                <div className="dropdown-trigger">
                  <button className="button" onClick={() => setShowPageSizePicker(!showPageSizePicker)}>
                    <span>{pageSize}</span>
                    <span className="icon is-small">
                      <i className="ion-iconic icon-chevron-down" />
                    </span>
                  </button>
                </div>
                <div className="dropdown-menu" id="dropdown-menu" role="menu">
                  <div className="dropdown-content is-clickable" onClick={() => setShowPageSizePicker(false)}>
                    <a className={"dropdown-item" + (pageSize === 5 && " is-active")}>5</a>
                    <a className={"dropdown-item" + (pageSize === 10 && " is-active")}>10</a>
                    <a className={"dropdown-item" + (pageSize === 25 && " is-active")}>25</a>
                  </div>
                </div>
              </div>
            </nav>
          </>
        )
      )}
    </Layout>
  );
}
