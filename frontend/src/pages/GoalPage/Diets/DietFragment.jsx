import { useContext, useEffect, useState } from "react";
import DietsContext from "../../../store/Diets/DietsContext";
import useInput from "../../../hooks/useInput";
import { IconButton, Stack, TextField, Typography } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import DoneIcon from "@mui/icons-material/Done";
import CreateButton from "../../../components/CreateButton/CreateButton";
import { uid } from "react-uid";
import { random } from "@mui/x-data-grid-generator";
import Diet from "./Diet";

const DietFragment = ({ sx }) => {
  const { diets, updateName, create, getById, del } = useContext(DietsContext);

  const [dietInEdit, setDietInEdit] = useState(null);
  const name = useInput("Новая диета", (str) =>
    str !== "" && str !== " " && str !== null
      ? { isValid: true, err: null }
      : { isValid: false, err: "Неверный ввод" }
  );

  useEffect(() => {
    if (!dietInEdit) return;
    name.setValue(dietInEdit.name);
  }, [dietInEdit]);

  const handleSubmit = (e) => {
    e.preventDefault();

    if (dietInEdit) {
      updateName(dietInEdit.id, name.value);
      setDietInEdit(null);
    } else {
      create({
        // это чисто для идентификации тренировок, которые только что были созданы, но не сохранены в БД
        id: uid(name.value + random(1, 10000000)),
        name: name.value,
        exercises: [],
      });
    }
    resetInputs();
  };

  const resetInputs = () => {
    name.reset();
  };

  const handleUpdateDiet = (id) => {
    setDietInEdit(getById(id));
  };

  const handleDeleteDiet = (id) => {
    del(id);
  };

  return (
    <Stack sx={{ gap: 2, ...sx }}>
      <Typography sx={{ fontSize: 20 }}>Диеты</Typography>
      <form onSubmit={handleSubmit}>
        <TextField
          label="Название диеты"
          value={name.value}
          onChange={name.onchange}
          error={name.error !== null}
          helperText={name.error}
        />
        {dietInEdit ? (
          <IconButton type="submit" sx={{ marginTop: 1 }}>
            <DoneIcon color="success" />
          </IconButton>
        ) : (
          <CreateButton sx={{ marginLeft: 2 }} type="submit">
            <AddIcon />
          </CreateButton>
        )}
      </form>
      {diets.length !== 0 && (
        <Stack sx={{ gap: 4 }}>
          {diets.map((diet, index) => (
            <Diet
              key={index}
              id={diet.id}
              name={diet.name}
              addedProducts={diet?.products ?? []}
              addedDishes={diet?.dishes ?? []}
              onUpdate={handleUpdateDiet}
              onDelete={handleDeleteDiet}
            />
          ))}
        </Stack>
      )}
    </Stack>
  );
};

export default DietFragment;
