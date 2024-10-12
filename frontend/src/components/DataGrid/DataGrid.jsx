import { useState } from "react";
import "./DataGrid.css"; // Импортируем стили

const DataGrid = () => {
  const [data, setData] = useState([]);
  const [formData, setFormData] = useState({ id: "", name: "", age: "" });
  const [isEditing, setIsEditing] = useState(false);
  const [columnWidths, setColumnWidths] = useState({ name: 150, age: 100 });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (isEditing) {
      setData(data.map((item) => (item.id === formData.id ? formData : item)));
      setIsEditing(false);
    } else {
      setData([...data, { ...formData, id: 1 }]);
    }
    setFormData({ id: "", name: "", age: "" });
  };

  const handleEdit = (item) => {
    setFormData(item);
    setIsEditing(true);
  };

  const handleDelete = (id) => {
    setData(data.filter((item) => item.id !== id));
  };

  const handleMouseDown = (column) => (e) => {
    const startX = e.clientX;
    const startWidth = columnWidths[column];

    const handleMouseMove = (e) => {
      const newWidth = startWidth + (e.clientX - startX);
      setColumnWidths((prev) => ({ ...prev, [column]: newWidth }));
    };

    const handleMouseUp = () => {
      document.removeEventListener("mousemove", handleMouseMove);
      document.removeEventListener("mouseup", handleMouseUp);
    };

    document.addEventListener("mousemove", handleMouseMove);
    document.addEventListener("mouseup", handleMouseUp);
  };

  return (
    <div>
      <h1>Custom DataGrid</h1>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          name="name"
          placeholder="Name"
          value={formData.name}
          onChange={handleChange}
          required
        />
        <input
          type="number"
          name="age"
          placeholder="Age"
          value={formData.age}
          onChange={handleChange}
          required
        />
        <button type="submit">{isEditing ? "Update" : "Add"}</button>
      </form>

      <table>
        <thead>
          <tr>
            <th style={{ width: columnWidths.name }}>
              Name
              <span className="resizer" onMouseDown={handleMouseDown("name")} />
            </th>
            <th style={{ width: columnWidths.age }}>
              Age
              <span className="resizer" onMouseDown={handleMouseDown("age")} />
            </th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {data.map((item) => (
            <tr key={item.id}>
              <td>{item.name}</td>
              <td>{item.age}</td>
              <td>
                <button onClick={() => handleEdit(item)}>Edit</button>
                <button onClick={() => handleDelete(item.id)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default DataGrid;
