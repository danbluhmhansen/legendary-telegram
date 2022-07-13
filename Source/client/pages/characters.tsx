import Head from 'next/head';
import { useEffect, useState } from 'react';
import Layout, { siteTitle } from '../components/layout';
import Pagination from '../components/pagination';
import SortTh from '../components/sortable-table-header';
import { ODataCollectionResponse, queryOData, SortDirection } from '../lib/odata';

interface Character {
  id: string;
  name: string;
}

export default function Characters() {
  const [characters, setCharacters] = useState<Character[]>();
  const [count, setCount] = useState<number>();
  const [isLoading, setLoading] = useState(false);
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [idSort, setIdSort] = useState(SortDirection.none);
  const [nameSort, setNameSort] = useState(SortDirection.none);

  useEffect(() => {
    const uri = queryOData('characters', '1.0', true, (page - 1) * pageSize, pageSize, [
      ['Id', idSort],
      ['Name', nameSort],
    ]);

    if (!characters) setLoading(true);
    fetch(uri)
      .then((res) => res.json())
      .then((data: ODataCollectionResponse<Character>) => {
        setCharacters(data.value);
        setCount(data['@odata.count']);
        setLoading(false);
      });
  }, [page, pageSize, idSort, nameSort]);

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
                  <SortTh sort={idSort} setSort={setIdSort} />
                  <SortTh sort={nameSort} setSort={setNameSort} />
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
            <Pagination count={count} page={page} setPage={setPage} pageSize={pageSize} setPageSize={setPageSize} />
          </>
        )
      )}
    </Layout>
  );
}
