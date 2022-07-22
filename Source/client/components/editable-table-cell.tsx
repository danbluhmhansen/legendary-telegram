import { useState } from 'react';

export default function EditTd({
  value,
  onEdit,
}: {
  value: string | number | readonly string[] | undefined;
  onEdit: (value: string | number | readonly string[] | undefined) => void;
}) {
  const [editing, setEditing] = useState(false);

  return (
    <td onClick={() => setEditing(true)}>
      {editing ? (
        <input
          className="input"
          type="text"
          autoFocus
          value={value}
          onBlur={() => setEditing(false)}
          onChange={(e) => onEdit(e.target.value)}
        />
      ) : (
        value
      )}
    </td>
  );
}
