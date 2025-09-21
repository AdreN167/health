import AdminLayout from "../../components/Layout/AdminLayout";
import { Stack, TextField } from "@mui/material";
import { useContext, useEffect, useState } from "react";
import useInput from "../../hooks/useInput";
import AddIcon from "@mui/icons-material/Add";
import UploadButton from "../../components/UploadButton/UploadButton";
import AuthContext from "../../store/AuthContext";
import DataGrid from "../../components/DataGrid/DataGrid";
import CreateButton from "../../components/CreateButton/CreateButton";
import UpdateButton from "../../components/UpdateButton/UpdateButton";
import EditOutlinedIcon from "@mui/icons-material/EditOutlined";
import {
  createTrainer,
  deleteTrainer,
  getTrainers,
  updateTrainer,
} from "../../api/trainerService";
import TrainerRow from "../../components/DataGrid/TrainerRow/TrainerRow";

const errMsg = "Ошибка ввода";

const gridHeaders = [
  { key: "name", value: "Название" },
  { key: "imageUrl", value: "Картинка", isUnSortable: true },
  { key: "edit", value: "", isUnSortable: true },
];

const TrainerPage = () => {
  const { token } = useContext(AuthContext);
  const name = useInput("Тренажер", (str) =>
    str !== "" && str !== " " && str !== null
      ? { isValid: true, err: null }
      : { isValid: false, err: errMsg }
  );
  const file = useInput(null, (file) =>
    file.type.split("/")[0] !== "image"
      ? { isValid: false, err: "Нужна картинка" }
      : { isValid: true, err: null }
  );
  const [fetchedData, setFetchedData] = useState([]);
  const [rowInEdit, setRowInEdit] = useState(null);

  useEffect(() => {
    get();
  }, []);

  const get = async () => {
    try {
      const data = await getTrainers(token);
      setFetchedData(data);
    } catch (err) {
      console.log(err);
    }
  };

  useEffect(() => {
    if (!rowInEdit) return;
    name.setValue(rowInEdit.name);
  }, [rowInEdit]);

  const createFormData = () => {
    const formData = new FormData();

    formData.append("name", name.value);
    if (file) {
      formData.append("image", file.value);
    }

    return formData;
  };

  const resetInputs = () => {
    name.reset();
    file.reset();
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const formData = createFormData();

    try {
      if (rowInEdit) {
        formData.append("id", rowInEdit.id);
        await updateTrainer(token, formData);
        setRowInEdit(null);
      } else {
        await createTrainer(token, formData);
      }
    } catch (err) {
      console.log(err);
    } finally {
      resetInputs();
    }

    await get();
  };

  const handleEdit = async (id) => {
    setRowInEdit(fetchedData.find((item) => item.id === id));
  };

  const handleDelete = async (id) => {
    try {
      await deleteTrainer(token, id);
    } catch (err) {
      console.log(err);
    }

    await get();
  };

  return (
    <AdminLayout>
      <form onSubmit={handleSubmit}>
        <Stack direction="row" sx={{ gap: 2 }}>
          <TextField
            label="Название"
            value={name.value}
            onChange={name.onchange}
            error={name.error !== null}
            helperText={name.error}
          />
          <UploadButton
            fileName={file.value && file.value.name}
            onChange={file.onchange}
            reset={file.reset}
          >
            Файл
          </UploadButton>
          {rowInEdit ? (
            <UpdateButton
              sx={{ marginLeft: "auto", marginRight: 4 }}
              type="submit"
            >
              <EditOutlinedIcon />
            </UpdateButton>
          ) : (
            <CreateButton
              sx={{ marginLeft: "auto", marginRight: 4 }}
              type="submit"
            >
              <AddIcon />
            </CreateButton>
          )}
        </Stack>
      </form>
      <DataGrid
        headers={gridHeaders}
        minCellWidth={50}
        data={fetchedData}
        component={TrainerRow}
        onEditClick={handleEdit}
        onDeleteClick={handleDelete}
        rowInEdit={rowInEdit}
      ></DataGrid>
    </AdminLayout>
  );
};

export default TrainerPage;
