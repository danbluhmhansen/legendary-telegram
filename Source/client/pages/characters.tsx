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

  let pages = Array.from(Array((count ? Math.ceil(count / pageSize) : 1) + 1).keys()).slice(1);

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
            <div className="columns">
              <div className="column">
                <div className="field has-addons">
                  <p className="control">
                    <button className="button" disabled={page === 1} onClick={() => setPage(1)}>
                      First
                    </button>
                  </p>
                  <p className="control">
                    <button className="button" disabled={page === 1} onClick={() => setPage(page - 1)}>
                      Prev
                    </button>
                  </p>
                  {pages.map(p => (
                    <p key={p} className="control">
                      <button className="button" disabled={page === p} onClick={() => setPage(p)}>
                        {p}
                      </button>
                    </p>
                  ))}
                  <p className="control">
                    <button className="button" disabled={page === pages.at(-1)} onClick={() => setPage(page + 1)}>
                      Next
                    </button>
                  </p>
                  <p className="control">
                    <button className="button" disabled={page === pages.at(-1)} onClick={() => setPage(pages.at(-1) ?? 1)}>
                      Last
                    </button>
                  </p>
                </div>
              </div>
              <div className="column">
                <div className="dropdown is-active">
                  <div className="dropdown-trigger">
                    <button className="button">
                      <span>5</span>
                      <span className="icon is-small"><i className="ion-iconic icon-chevron-down" /></span>
                    </button>
                  </div>
                  <div className="dropdown-menu" role="menu">
                    <div className="dropdown-content">
                      <a className="dropdown-item">5</a>
                      <a className="dropdown-item">10</a>
                      <a className="dropdown-item">25</a>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </>
        )
      )}
    </Layout>
  );
}
