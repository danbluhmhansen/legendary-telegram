import { ColumnDef, flexRender, getCoreRowModel, useReactTable } from "@tanstack/react-table";
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

const columns: ColumnDef<Character>[] = [
  {
    accessorKey: 'id',
    cell: info => info.getValue(),
    footer: info => info.column.id,
  },
  {
    accessorKey: 'name',
    cell: info => info.getValue(),
    footer: info => info.column.id,
  },
]

export default function Characters() {
  const [characters, setCharacters] = useState<Character[]>();
  const [isLoading, setLoading] = useState(false);
  const table = useReactTable({
    data: characters,
    columns,
    getCoreRowModel: getCoreRowModel(),
  });

  useEffect(() => {
    setLoading(true);
    fetch('https://localhost:7000/api/characters?api-version=1.0')
      .then((res) => res.json())
      .then((data: ODataCollectionResponse<Character>) => {
        setCharacters(data.value);
        setLoading(false);
      });
  }, []);

  return (
    <Layout>
      <Head><title>{siteTitle} - Characters</title></Head>
      <h1 className='title'>Characters</h1>
      {isLoading ? <progress className='progress is-primary' /> :
      characters &&
      <table className='table'>
        <thead>
          {table.getHeaderGroups().map(headerGroup => (
            <tr key={headerGroup.id}>
              {headerGroup.headers.map(header => (
              <th key={header.id}>
                {header.isPlaceholder
                  ? undefined
                  : flexRender(
                    header.column.columnDef.header,
                    header.getContext()
                  )}
              </th>
              ))}
            </tr>
          ))}
        </thead>
        <tbody>
          {table.getRowModel().rows.map(row => (
            <tr key={row.id}>
              {row.getVisibleCells().map(cell => (
                <td key={cell.id}>
                  {flexRender(cell.column.columnDef.cell, cell.getContext())}
                </td>
              ))}
            </tr>
          ))}
        </tbody>
        <tfoot>
          {table.getFooterGroups().map(footerGroup => (
            <tr key={footerGroup.id}>
              {footerGroup.headers.map(header => (
                <th key={header.id}>
                  {header.isPlaceholder
                    ? undefined
                    : flexRender(
                        header.column.columnDef.footer,
                        header.getContext()
                      )}
                </th>
              ))}
            </tr>
          ))}
        </tfoot>
      </table>}
    </Layout>
  )
}

