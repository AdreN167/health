import AdminLayout from "../../components/Layout/AdminLayout";
import { MenuItem, Select, Stack, TextField } from "@mui/material";
import { useContext, useEffect, useState } from "react";
import useInput from "../../hooks/useInput";
import AddIcon from "@mui/icons-material/Add";
import AuthContext from "../../store/AuthContext";
import DataGrid from "../../components/DataGrid/DataGrid";
import CreateButton from "../../components/CreateButton/CreateButton";
import UpdateButton from "../../components/UpdateButton/UpdateButton";
import EditOutlinedIcon from "@mui/icons-material/EditOutlined";
import { getTrainers } from "../../api/trainerService";
import {
  createExercise,
  deleteExercise,
  getExercises,
  updateExercise,
} from "../../api/exerciseService";
import ExerciseRow from "../../components/DataGrid/ExerciseRow/ExerciseRow";

const errMsg = "Ошибка ввода";

const gridHeaders = [
  { key: "name", value: "Название" },
  { key: "description", value: "Описание" },
  { key: "caloriesBurned", value: "Сжигаемые калории/повторение" },
  { key: "trainer", value: "Тренажер" },
  { key: "edit", value: "", isUnSortable: true },
];

const validateNumber = (num) =>
  num > 0 ? { isValid: true, err: null } : { isValid: false, err: errMsg };

const defaultOption = {
  id: 0,
  name: "Без тренажера",
};

const ExercisePage = () => {
  const { token } = useContext(AuthContext);
  const [options, setOptions] = useState([]);
  const name = useInput("Упражнение1", (str) =>
    str !== "" && str !== " " && str !== null
      ? { isValid: true, err: null }
      : { isValid: false, err: errMsg }
  );
  const description = useInput("Описание1", (str) =>
    str !== "" && str !== " " && str !== null
      ? { isValid: true, err: null }
      : { isValid: false, err: errMsg }
  );
  const caloriesBurned = useInput(1, validateNumber);
  const [trainer, setTrainer] = useState(defaultOption);
  const [fetchedData, setFetchedData] = useState([]);
  const [rowInEdit, setRowInEdit] = useState(null);

  useEffect(() => {
    get();
    getOptions();
  }, []);

  const get = async () => {
    try {
      const data = await getExercises(token);
      setFetchedData(data);
    } catch (err) {
      console.log(err);
    }
  };

  const getOptions = async () => {
    try {
      const fetchedOptions = await getTrainers(token);
      setOptions([
        defaultOption,
        ...fetchedOptions.map((opt) => ({ id: opt.id, name: opt.name })),
      ]);
    } catch (err) {
      console.log(err);
    }
  };

  useEffect(() => {
    if (!rowInEdit) return;
    name.setValue(rowInEdit.name);
    description.setValue(rowInEdit.description);
    caloriesBurned.setValue(rowInEdit.caloriesBurned);

    const tr = { ...defaultOption };
    if (rowInEdit.trainer) {
      tr.id = rowInEdit.trainer.id;
      tr.name = rowInEdit.trainer.name;
    } else {
      tr.id = 0;
      tr.name = "Без тренажера";
    }

    setTrainer(options.find((item) => item.id === tr.id));
  }, [rowInEdit]);

  const resetInputs = () => {
    name.reset();
    description.reset();
    caloriesBurned.reset();
    setTrainer(defaultOption);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    console.log(trainer.id === defaultOption.id ? null : trainer.id);
    let body = {
      name: name.value,
      description: description.value,
      caloriesBurned: caloriesBurned.value,
      trainerId: trainer.id === defaultOption.id ? null : trainer.id,
    };

    try {
      if (rowInEdit) {
        body = { id: rowInEdit.id, ...body };
        await updateExercise(token, body);
        setRowInEdit(null);
      } else {
        await createExercise(token, body);
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
      await deleteExercise(token, id);
    } catch (err) {
      console.log(err);
    }

    await get();
  };

  return (
    <AdminLayout>
      <form onSubmit={handleSubmit}>
        <Stack sx={{ gap: 2 }}>
          <Stack direction="row" sx={{ gap: 2 }}>
            <TextField
              label="Название"
              value={name.value}
              onChange={name.onchange}
              error={name.error !== null}
              helperText={name.error}
            />
            <TextField
              label="Сжигаемые калории"
              type="number"
              value={caloriesBurned.value}
              onChange={caloriesBurned.onchange}
              error={caloriesBurned.error !== null}
              helperText={caloriesBurned.error}
            />
            <Select
              placeholder={"Тренажер"}
              label={"Тренажер"}
              sx={{ maxWidth: 200, width: "100%" }}
              variant="outlined"
              value={trainer}
              onChange={(e) => setTrainer(e.target.value)}
            >
              {options.map((option) => {
                return (
                  <MenuItem key={option.id} value={option}>
                    {option.name}
                  </MenuItem>
                );
              })}
            </Select>
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

          <TextField
            label="Описание"
            multiline
            rows={8}
            value={description.value}
            onChange={description.onchange}
            error={description.error !== null}
            helperText={description.error}
            sx={{ minWidth: 500 }}
          />
        </Stack>
      </form>
      <DataGrid
        headers={gridHeaders}
        minCellWidth={50}
        data={fetchedData}
        component={ExerciseRow}
        onEditClick={handleEdit}
        onDeleteClick={handleDelete}
        rowInEdit={rowInEdit}
      ></DataGrid>
    </AdminLayout>
  );
};

export default ExercisePage;
