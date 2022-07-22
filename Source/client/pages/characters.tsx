import Head from 'next/head';
import { useEffect, useState } from 'react';
import EditTd from '../components/editable-table-cell';
import Layout, { siteTitle } from '../components/layout';
import Pagination, { PaginationContext } from '../components/pagination';
import SortTh from '../components/sortable-table-header';
import { ODataCollectionResponse, queryOData, SortDirection } from '../lib/odata';

interface Character {
  id: string;
  name: string;
}

export default function CharactersPage() {
  const [characters, setCharacters] = useState<Character[]>([]);
  const [isLoading, setLoading] = useState(false);

  const [pagination, setPagination] = useState({ count: 0, page: 1, pageSize: 10 });

  const { page, pageSize } = pagination;

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
        setPagination({ ...pagination, count: data['@odata.count'] ?? 0 });
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
                  <SortTh sort={idSort} setSort={setIdSort}>
                    Id
                  </SortTh>
                  <SortTh sort={nameSort} setSort={setNameSort}>
                    Name
                  </SortTh>
                </tr>
              </thead>
              <tbody>
                {characters?.map((character) => (
                  <tr key={character.id}>
                    <td>{character.id}</td>
                    <EditTd
                      value={character.name}
                      onEdit={(value) => {
                        if (typeof value !== 'string') return;
                        character.name = value;
                        setCharacters([...characters]);
                      }}
                    />
                  </tr>
                ))}
              </tbody>
            </table>
            <PaginationContext.Provider value={{ pagination, setPagination }}>
              <Pagination />
            </PaginationContext.Provider>
          </>
        )
      )}
    </Layout>
  );
}
