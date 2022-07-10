import Head from "next/head";
import { useEffect, useState } from "react";
import Layout, { siteTitle } from "../components/layout";

interface ODataCollectionResponse<T> {
  '@odata.context': string;
  '@odata.count'?: number;
  value: T[];
}

interface Character {
  id: string;
  name: string;
}

export default function Characters() {
  const [characters, setCharacters] = useState<ODataCollectionResponse<Character> | undefined>();
  const [isLoading, setLoading] = useState(false);

  useEffect(() => {
    setLoading(true);
    fetch('https://localhost:7000/api/characters?api-version=1.0')
      .then((res) => res.json())
      .then((data: ODataCollectionResponse<Character>) => {
        setCharacters(data);
        setLoading(false);
      });
  }, []);

  return (
    <Layout>
      <Head><title>{siteTitle} - Characters</title></Head>
      <h1 className='title'>Characters</h1>
      {isLoading ? <progress className='progress is-primary' /> :
      <table className='table'>
        <thead>
          <tr>
            <th>Id</th>
            <th>Name</th>
          </tr>
        </thead>
        <tbody>
          {characters && characters.value.map(character => {
            return <tr><td>{character.id}</td><td>{character.name}</td></tr>
          })}
        </tbody>
      </table>}
    </Layout>
  )
}

