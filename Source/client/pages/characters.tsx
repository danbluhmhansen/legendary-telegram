import Head from 'next/head';
import { useEffect, useState } from 'react';
import Layout, { siteTitle } from '../components/layout';
import { IoCaretDown, IoCaretUp } from 'react-icons/io5';

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

function Cycle(sort:SortDirection) {
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

function SortIcon(sort:SortDirection) {
  switch (sort) {
    case SortDirection.asc:
      return <IoCaretUp />
    case SortDirection.desc:
      return <IoCaretDown />
    default:
      break;
  }
}

function ODataSort(property:string, sort:SortDirection) {
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
  const [isLoading, setLoading] = useState(false);
  const [idSort, setIdSort] = useState(SortDirection.none);
  const [nameSort, setNameSort] = useState(SortDirection.none);

  useEffect(() => {
    let uri = 'https://localhost:7000/api/characters?api-version=1.0';

    uri += ODataSort('Id', idSort);
    uri += ODataSort('Name', nameSort);

    setLoading(true);
    fetch(uri)
      .then((res) => res.json())
      .then((data: ODataCollectionResponse<Character>) => {
        setCharacters(data.value);
        setLoading(false);
      });
  }, [idSort, nameSort]);

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
          <table className="table">
            <thead>
              <tr>
                <th onClick={() => setIdSort(Cycle(idSort))} className="is-clickable">Id{SortIcon(idSort)}</th>
                <th onClick={() => setNameSort(Cycle(nameSort))} className="is-clickable">Name{SortIcon(nameSort)}</th>
              </tr>
            </thead>
            <tbody>
              {characters.map(character => (
                <tr key={character.id}>
                  <td>{character.id}</td>
                  <td>{character.name}</td>
                </tr>
              ))}
            </tbody>
          </table>
        )
      )}
    </Layout>
  );
}
