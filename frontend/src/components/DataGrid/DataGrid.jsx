import { useState, useCallback, useEffect, useRef, useMemo } from "react";
import "./DataGrid.less";
import { apiBaseUrl } from "../../common/constants";
import RowWithImage from "./RowWithImage/RowWithImage";
import EdiatableRow from "./EditableRow/EditableRow";

const createHeaders = (headers) => {
  return headers.map((item) => ({
    key: item.key,
    value: item.value,
    isUnSortable: item.isUnSortable,
    ref: useRef(),
  }));
};

const DataGrid = ({
  headers,
  minCellWidth,
  data,
  component: RowComponent,
  onEditClick,
  onDeleteClick,
  rowInEdit,
}) => {
  const [sortConfig, setSortConfig] = useState({
    key: null,
    direction: "ascending",
  });
  const [tableHeight, setTableHeight] = useState("auto");
  const [activeIndex, setActiveIndex] = useState(null);
  const tableElement = useRef(null);
  const columns = createHeaders(headers);

  const sortedData = useMemo(() => {
    let sortableItems = [...data];
    if (sortConfig !== null) {
      sortableItems.sort((a, b) => {
        if (a[sortConfig.key] < b[sortConfig.key]) {
          return sortConfig.direction === "ascending" ? -1 : 1;
        }
        if (a[sortConfig.key] > b[sortConfig.key]) {
          return sortConfig.direction === "ascending" ? 1 : -1;
        }
        return 0;
      });
    }
    return sortableItems;
  }, [data, sortConfig]);

  const requestSort = (key) => {
    let direction = "ascending";
    if (
      sortConfig &&
      sortConfig.key === key &&
      sortConfig.direction === "ascending"
    ) {
      direction = "descending";
    }
    setSortConfig({ key, direction });
  };

  useEffect(() => {
    setTableHeight(tableElement.current.offsetHeight);
  }, []);

  const mouseDown = (index) => {
    setActiveIndex(index);
  };

  const mouseMove = useCallback(
    (e) => {
      const gridColumns = columns.map((col, i) => {
        if (i === activeIndex) {
          const width = e.clientX - col.ref.current.offsetLeft;

          if (width >= minCellWidth) {
            return `${width}px`;
          }
        }
        return `${col.ref.current.offsetWidth}px`;
      });

      tableElement.current.style.gridTemplateColumns = `${gridColumns.join(
        " "
      )}`;
    },
    [activeIndex, columns, minCellWidth]
  );

  const removeListeners = useCallback(() => {
    window.removeEventListener("mousemove", mouseMove);
    window.removeEventListener("mouseup", removeListeners);
  }, [mouseMove]);

  const mouseUp = useCallback(() => {
    setActiveIndex(null);
    removeListeners();
  }, [setActiveIndex, removeListeners]);

  useEffect(() => {
    if (activeIndex !== null) {
      window.addEventListener("mousemove", mouseMove);
      window.addEventListener("mouseup", mouseUp);
    }

    return () => {
      removeListeners();
    };
  }, [activeIndex, mouseMove, mouseUp, removeListeners]);

  const handleEdit = (id) => onEditClick(id);
  const handleDelete = (id) => onDeleteClick(id);

  return (
    <div className="container">
      <div className="table-wrapper">
        <table
          className="resizeable-table table"
          ref={tableElement}
          style={{
            gridTemplateColumns: columns
              .map((_) => "minmax(100px, 1fr)")
              .join(" "),
          }}
        >
          <thead className="thead">
            <tr className="tr">
              {columns.map(({ ref, value, key, isUnSortable }, i) => (
                <th
                  className="th"
                  ref={ref}
                  key={value}
                  style={{ cursor: !isUnSortable && "pointer" }}
                  onClick={() => (!isUnSortable ? requestSort(key) : null)}
                >
                  <span className="span">{value}</span>
                  <div
                    style={{ height: tableHeight }}
                    onMouseDown={() => mouseDown(i)}
                    className={`resize-handle ${
                      activeIndex === i ? "active" : "idle"
                    }`}
                  />
                </th>
              ))}
            </tr>
          </thead>
          <tbody className="tbody">
            {sortedData &&
              sortedData.map((item, i) =>
                item.imageUrl !== null && item.imageUrl !== undefined ? (
                  <tr
                    key={i}
                    className={
                      rowInEdit?.id === item.id ? "table-wrapper__in-edit" : ""
                    }
                  >
                    <EdiatableRow
                      id={item.id}
                      onEditClick={handleEdit}
                      onDeleteClick={handleDelete}
                    >
                      <RowWithImage
                        imageUrl={
                          item.imageUrl !== "" ? apiBaseUrl + item.imageUrl : ""
                        }
                      >
                        <RowComponent content={item} />
                      </RowWithImage>
                    </EdiatableRow>
                  </tr>
                ) : (
                  <tr
                    key={i}
                    className={
                      rowInEdit?.id === item.id ? "table-wrapper__in-edit" : ""
                    }
                  >
                    <EdiatableRow
                      id={item.id}
                      onEditClick={handleEdit}
                      onDeleteClick={handleDelete}
                    >
                      <RowComponent content={item} />
                    </EdiatableRow>
                  </tr>
                )
              )}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default DataGrid;
