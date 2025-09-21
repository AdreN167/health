import { Stack, TextField } from "@mui/material";
import AdminLayout from "../../components/Layout/AdminLayout";
import { useContext, useEffect, useState } from "react";
import useInput from "../../hooks/useInput";
import AddIcon from "@mui/icons-material/Add";
import UploadButton from "../../components/UploadButton/UploadButton";
import { getProducts } from "../../api/productService";
import AuthContext from "../../store/AuthContext";
import DataGrid from "../../components/DataGrid/DataGrid";
import CreateButton from "../../components/CreateButton/CreateButton";
import UpdateButton from "../../components/UpdateButton/UpdateButton";
import EditOutlinedIcon from "@mui/icons-material/EditOutlined";
import {
  createDish,
  deleteDish,
  getDishes,
  updateDish,
  updateListOfDishProducts,
} from "../../api/dishService";
import DishRow from "../../components/DataGrid/DishRow/DishRow";
import ListWithAddButton from "../../components/ListWithAddButton/ListWithAddButton";

const errMsg = "Ошибка ввода";

const gridHeaders = [
  { key: "name", value: "Название" },
  { key: "description", value: "Описание" },
  { key: "products", value: "Продукты" },
  { key: "imageUrl", value: "Картинка", isUnSortable: true },
  { key: "edit", value: "", isUnSortable: true },
];

const DishPage = () => {
  const { token } = useContext(AuthContext);
  const name = useInput("Блюдо1", (str) =>
    str !== "" && str !== " " && str !== null
      ? { isValid: true, err: null }
      : { isValid: false, err: errMsg }
  );
  const description = useInput("Описание", () => ({
    isValid: true,
    err: null,
  }));
  const file = useInput(null, (file) =>
    file.type.split("/")[0] !== "image"
      ? { isValid: false, err: "Нужна картинка" }
      : { isValid: true, err: null }
  );
  const [products, setProducts] = useState([]);
  const [fetchedData, setFetchedData] = useState([]);
  const [rowInEdit, setRowInEdit] = useState(null);
  const [options, setOptions] = useState([]);

  const get = async () => {
    try {
      const data = await getDishes(token);
      setFetchedData(data);
    } catch (err) {
      console.log(err);
    }
  };

  useEffect(() => {
    get();
    const getOptions = async () => {
      try {
        const opt = await getProducts(token);
        setOptions(opt);
      } catch (err) {
        console.log(err);
      }
    };
    getOptions();
  }, []);

  useEffect(() => {
    if (!rowInEdit) return;

    name.setValue(rowInEdit.name);
    description.setValue(rowInEdit.description);
    setProducts(rowInEdit.products);
  }, [rowInEdit]);

  const createFormData = () => {
    const formData = new FormData();

    formData.append("name", name.value);
    formData.append("description", description.value);
    if (file) {
      formData.append("image", file.value);
    }

    return formData;
  };

  const resetInputs = () => {
    name.reset();
    description.reset();
    file.reset();
    setProducts([]);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const formData = createFormData();

    try {
      const newList = products?.reduce((acc, obj) => {
        acc[obj.id] = obj.weight;
        return acc;
      }, {});
      console.log(newList);
      if (rowInEdit) {
        formData.append("id", rowInEdit.id);
        await updateDish(token, formData);
        await updateListOfDishProducts(token, {
          id: rowInEdit.id,
          productsWithWeight: newList,
        });
        setRowInEdit(null);
      } else {
        const id = await createDish(token, formData);
        await updateListOfDishProducts(token, {
          id: id,
          productsWithWeight: newList,
        });
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
      await deleteDish(token, id);
    } catch (err) {
      console.log(err);
    }

    await get();
  };

  const handleListOfProductsChange = (list) => {
    setProducts(
      list.map((x) => ({ id: x.id, weight: x.value, name: x.label }))
    );
  };

  return (
    <AdminLayout>
      <Stack direction="column" sx={{ gap: 4, height: 289 }}>
        <form onSubmit={handleSubmit}>
          <Stack direction="row" sx={{ gap: 2 }}>
            <Stack direction="column" sx={{ gap: 2 }}>
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
            <Stack direction="column" sx={{ width: "100%", height: "100%" }}>
              <ListWithAddButton
                caption={"Список продуктов"}
                options={options.map((x) => ({ key: x.name, value: x.id }))}
                measurement={"г"}
                values={
                  products &&
                  products.map((x) => ({
                    key: x.id,
                    value: x.weight,
                    label: x.name,
                  }))
                }
                onChange={handleListOfProductsChange}
              />
            </Stack>
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
          component={DishRow}
          onEditClick={handleEdit}
          onDeleteClick={handleDelete}
          rowInEdit={rowInEdit}
        ></DataGrid>
      </Stack>
    </AdminLayout>
  );
};

export default DishPage;
