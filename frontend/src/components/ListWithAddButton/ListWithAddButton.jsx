import {
  IconButton,
  MenuItem,
  Select,
  Stack,
  TextField,
  Typography,
} from "@mui/material";
import DoneRoundedIcon from "@mui/icons-material/DoneRounded";
import EditOutlinedIcon from "@mui/icons-material/EditOutlined";
import DeleteForeverOutlinedIcon from "@mui/icons-material/DeleteForeverOutlined";
import { useEffect, useState } from "react";
import useInput from "../../hooks/useInput";

const validateNumber = (num) =>
  num > 0
    ? { isValid: true, err: null }
    : { isValid: false, err: "Ошибка ввода" };

const ListWithAddButton = ({
  caption,
  values,
  options,
  onChange,
  measurement,
}) => {
  const selectedItemId = useInput(null, (vall) =>
    data.find((x) => x.key !== selectedItemId.value) === undefined
      ? { isValid: false, err: "Уже есть" }
      : { isValid: true, err: null }
  );
  const parametr = useInput(1, validateNumber);
  const [rowInEdit, setRowInEdit] = useState(null);
  const [data, setData] = useState([]);

  useEffect(() => {
    if (options) {
      selectedItemId.setValue(options.length > 0 ? options[0].value : null);
    }
  }, [options]);

  useEffect(() => {
    setData(values ? values : []);
  }, [values]);

  const handleClick = (e) => {
    let newData;
    if (!rowInEdit) {
      const label = options.find((x) => x.value === selectedItemId.value).key;
      if (data.find((x) => x.key === selectedItemId.value) === undefined) {
        newData = [
          {
            key: selectedItemId.value,
            value: parametr.value,
            label: label,
          },
          ...data,
        ];
        setData(newData);
      }
    } else {
      const old = data.find((x) => x.key === selectedItemId.value);
      const filteredData = data.filter((x) => x.key !== rowInEdit.key);
      const newKey =
        old.key !== selectedItemId.value ? selectedItemId.value : rowInEdit.key;
      newData = [
        {
          key: newKey,
          value: parametr.value,
          label: options.find((x) => x.value === newKey).key,
        },
        ...filteredData,
      ];
      setData(newData);
      setRowInEdit(null);
    }
    if (newData) {
      parametr.reset();
      onChange(
        newData.map((x) => ({ id: x.key, value: x.value, label: x.label }))
      );
    }
  };

  const handleEdit = (id) => {
    const inEdit = data.find((x) => x.key === id);
    setRowInEdit(inEdit);
    parametr.setValue(inEdit.value);
    selectedItemId.setValue(inEdit.key);
  };

  const handleDelete = (id) => {
    const newData = data.filter((x) => x.key !== id);
    setData(newData);
    onChange(
      newData.map((x) => ({
        key: x.id,
        value: x.weight,
        label: x.name,
      }))
    );
  };

  return (
    <>
      <Typography>{caption}</Typography>
      <Stack direction="row" sx={{ gap: 2 }}>
        <Select
          placeholder={caption}
          label={caption}
          sx={{ maxWidth: 200, width: "100%" }}
          variant="standard"
          value={selectedItemId.value}
          onChange={(e) => selectedItemId.setValue(e.target.value)}
          onError={selectedItemId.error !== null}
        >
          {options.map((option) => (
            <MenuItem value={option.value}>{option.key}</MenuItem>
          ))}
        </Select>
        <TextField
          label={measurement ? measurement : "measurement"}
          type="number"
          sx={{ maxWidth: 120, width: "100%" }}
          value={parametr.value}
          onChange={parametr.onchange}
          error={parametr.error !== null}
        />
        <IconButton
          onClick={handleClick}
          sx={{ width: 56, height: 56, borderRadius: 2 }}
        >
          <DoneRoundedIcon color="success" />
        </IconButton>
      </Stack>
      <Stack direction="column" sx={{ marginTop: 1, gap: 2, maxWidth: 400 }}>
        {data &&
          data.map((item, i) => (
            <Stack
              direction="row"
              sx={{
                width: "100%",
                maxWidth: 400,
                gap: 2,
                bgcolor:
                  rowInEdit && rowInEdit.key === item.key
                    ? "#8d8d8d2d"
                    : "white",
              }}
            >
              <Stack
                key={i}
                sx={{ gap: 2, width: "100%", maxWidth: 335 }}
                direction="row"
                justifyContent="space-between"
              >
                <Typography
                  sx={{
                    maxWidth: 200,
                    textOverflow: "ellipsis",
                    whiteSpace: "nowrap",
                    overflow: "hidden",
                  }}
                >
                  {item.label}
                </Typography>
                <Typography
                  sx={{ width: "100%", maxWidth: 120, textAlign: "center" }}
                >
                  {item.value} {measurement}
                </Typography>
              </Stack>
              <Stack direction="row" sx={{ gap: 1 }}>
                <IconButton
                  sx={{ padding: 0 }}
                  onClick={() => handleEdit(item.key)}
                >
                  <EditOutlinedIcon fontSize="small" />
                </IconButton>
                <IconButton
                  sx={{ padding: 0 }}
                  onClick={() => handleDelete(item.key)}
                >
                  <DeleteForeverOutlinedIcon fontSize="small" />
                </IconButton>
              </Stack>
            </Stack>
          ))}
      </Stack>
    </>
  );
};
export default ListWithAddButton;
