import Head from 'next/head';
import { useEffect, useState } from 'react';
import EditTd from '../components/editable-table-cell';
import Layout, { siteTitle } from '../components/layout';
import Pagination from '../components/pagination';
import SortTh from '../components/sortable-table-header';
import { ODataCollectionResponse, queryOData, SortDirection } from '../lib/odata';

interface Character {
  id: string;
  name: string;
}

export default function CharactersPage() {
  const [characters, setCharacters] = useState<Character[]>([]);
  const [count, setCount] = useState(0);
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
        setCount(data['@odata.count'] ?? 0);
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
                    <EditTd value={character.name} onEdit={(value) => {
                      character.name = value;
                      setCharacters([...characters]);
                      console.log(characters);
                    }} />
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
