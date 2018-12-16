// RedUI v1.0.0
// Copyright (c) 2013 Artem Kondratyev
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
/// <reference path="../Scripts/typings/jquery/jquery.d.ts"/>
/// <reference path="../Scripts/typings/jqueryui/jqueryui.d.ts"/>
/// <reference path="../Scripts/typings/mustache/mustache.d.ts"/>
/// <reference path="../redui/redui-1.0.0-interfaces.ts"/>
"use strict";
(function (window) {
    // *****************************************************************************************
    // *                             Constants                                                 *
    // *****************************************************************************************
    // Id of DIV for RedUI application
    var REDUI_APPLICATION_ID = "#redui_application";

    // Name of the RedUI application data attribute which contains the ui model URI
    var REDUI_APPLICATION_MODEL_DATA_NAME = "model";

    // Name of the RedUI application data attribute which contains the templates base url
    var REDUI_APPLICATION_TEMPLATESBASEURL_DATA_NAME = "templatesbaseurl";

    // Basic control types
    var REDUI_CONTROL_TYPE_WINDOW = "window";
    var REDUI_CONTROL_TYPE_TEXTBOX = "textbox";
    var REDUI_CONTROL_TYPE_TEXTAREA = "textarea";
    var REDUI_CONTROL_TYPE_CHECKBOX = "checkbox";
    var REDUI_CONTROL_TYPE_COMBOBOX = "combobox";
    var REDUI_CONTROL_TYPE_STATICTEXT = "statictext";
    var REDUI_CONTROL_TYPE_IMAGE = "image";
    var REDUI_CONTROL_TYPE_GROUPBOX = "groupbox";
    var REDUI_CONTROL_TYPE_BUTTON = "button";
    var REDUI_CONTROL_TYPE_OBJECTSELECTOR = "objectselector";
    var REDUI_CONTROL_TYPE_PAGER = "pager";

    // Grid view control types
    var REDUI_CONTROL_TYPE_GRIDVIEW = "gridview";
    var REDUI_CONTROL_TYPE_GRIDVIEWROW = "gridviewrow";

    var REDUI_CONTROL_TYPE_GRIDVIEWTEXTBOXCOLUMN = "gridviewtextboxcolumn";
    var REDUI_CONTROL_TYPE_GRIDVIEWCHECKBOXCOLUMN = "gridviewcheckboxcolumn";
    var REDUI_CONTROL_TYPE_GRIDVIEWCOMBOBOXCOLUMN = "gridviewcomboboxcolumn";
    var REDUI_CONTROL_TYPE_GRIDVIEWSTATICTEXTCOLUMN = "gridviewstatictextcolumn";
    var REDUI_CONTROL_TYPE_GRIDVIEWOBJECTSELECTORCOLUMN = "gridviewobjectselectorcolumn";

    var REDUI_CONTROL_TYPE_GRIDVIEWTEXTBOXCELL = "gridviewtextboxcell";
    var REDUI_CONTROL_TYPE_GRIDVIEWCHECKBOXCELL = "gridviewcheckboxcell";
    var REDUI_CONTROL_TYPE_GRIDVIEWCOMBOBOXCELL = "gridviewcomboboxcell";
    var REDUI_CONTROL_TYPE_GRIDVIEWSTATICTEXTCELL = "gridviewstatictextcell";
    var REDUI_CONTROL_TYPE_GRIDVIEWOBJECTSELECTORCELL = "gridviewobjectselectorcell";

    // Tree view control types
    var REDUI_CONTROL_TYPE_TREEVIEW = "treeview";
    var REDUI_CONTROL_TYPE_TREEVIEWNODE = "treeviewnode";

    // Events
    var REDUI_EVENT_CHANGE = "change.redui";
    var REDUI_EVENT_CLICK = "click.redui";
    var REDUI_EVENT_WINDOW_CLOSING = "redui_window_closing";
    var REDUI_EVENT_WINDOW_CLOSED = "redui_window_closed";
    var REDUI_EVENT_GRIDVIEW_ROWSELECTED = "redui_gridview_rowselected";
    var REDUI_EVENT_TREEVIEW_NODESELECTED = "redui_treeview_nodeselected";
    var REDUI_EVENT_TREEVIEWNODE_EXPANDED = "redui_treeviewnode_expanded";
    var REDUI_EVENT_PAGER_PAGECHANGED = "redui_pager_pagechanged";

    // Various constants
    var REDUI_WINDOW_TITLE_HEIGHT = 32;
    var REDUI_TEXTAREA_COLUMNS_DEFAULT = 80;
    var REDUI_TEXTAREA_ROWS_DEFAULT = 10;
    var ROWS_TO_RENDER_IN_ONE_BATCH = 10;

    // Special CSS classes
    var REDUI_CLASS_HIDDEN = "redui-hidden";
    var REDUI_CLASS_FOCUSABLE = "redui-focusable";
    var REDUI_CLASS_ONCHANGEUPDATESDATA = "redui-onchangeupdatesdata";
    var REDUI_CLASS_DIALOGBUTTON = "redui-dialogbutton";
    var REDUI_CLASS_MODALWINDOW = "redui-modalwindow";
    var REDUI_CLASS_ONCHANGEUDPATESDATA = "redui-onchangeupdatesdata";
    var REDUI_CLASS_OBJECTSELECTOR = "redui-objectselector";
    var REDUI_CLASS_OBJECTSELECTORCELL = "redui-gridviewobjectselectorcell";
    var REDUI_CLASS_GRIDVIEW_GRIDVIEWROW = "redui-gridviewrow";
    var REDUI_CLASS_GRIDVIEW_SENTINELGRIDVIEWROW = "redui-sentinelgridviewrow";
    var REDUI_CLASS_GRIDVIEW_GRIDVIEWROW_SELECTED = "redui-gridviewrow-selected";
    var REDUI_CLASS_TREEVIEWNODE_OUTER = "redui-treeviewnode-outer";
    var REDUI_CLASS_TREEVIEWNODE_SELECTED = "redui-treeviewnode-selected";
    var REDUI_CLASS_TREEVIEWNODE_COLLAPSED = "redui-treeviewnode-collapsed";
    var REDUI_CLASS_TREEVIEWNODE_EMPTY = "redui-treeviewnode-empty";
    var REDUI_CLASS_TREEVIEWNODE_INNERNODES = "redui-treeviewnode-innernodes";

    // *****************************************************************************************
    // *                             Static variables                                          *
    // *****************************************************************************************
    // Red UI application
    var _reduiApplication = $(REDUI_APPLICATION_ID);
    if (!_reduiApplication) {
        throw "Application is not found. Please make sure you defined the RedUI application (<div id='redui_application'.../>)";
    }

    // Application UI model
    var _model = loadModel();

    // Templates base URL
    var _templatesBaseUrl = _reduiApplication.data(REDUI_APPLICATION_TEMPLATESBASEURL_DATA_NAME);
    if (!_templatesBaseUrl) {
        throw "Base URL for templates is not set. (<div id='redui_application' ... data-templatesbaseurl='...' ... />)";
    }

    // Templates
    var _templates = {};

    // Combobox options
    var _optionsBag = {};

    // Object sources
    var _objectSources = {};

    // *****************************************************************************************
    // *                             Helpers                                                   *
    // *****************************************************************************************
    /*
    Returns the new id for the control.
    */
    var getNewId = (function () {
        // Last generated id.
        var _lastId = 0;
        return function () {
            _lastId++;
            return _lastId;
        };
    })();

    /*
    Returns the modal depth counter.
    */
    var modalDepthCounter = (function () {
        // Current modal depth.
        var _depth = 0;
        var counter = {
            increase: function () {
                _depth++;
                return _depth;
            },
            decrease: function () {
                _depth--;
                return _depth;
            },
            current: function () {
                return _depth;
            }
        };
        return counter;
    })();

    /*
    Finds am element in an array.
    @arr the array
    @predicate function that tests an array element.
    The first array element for which the predicate returns true is returned.
    If predicate does not return true for any element, null is returned.
    */
    function findInArray(arr, predicate) {
        for (var i = 0, len = arr.length; i < len; i++) {
            if (predicate(arr[i])) {
                return arr[i];
            }
        }

        return null;
    }

    /*
    Returns the sourceObject nested property value.
    Example:    a = { b: { c : "hi" } };
    getNestedPropertyValue(a, "b.c") returns "hi".
    @sourceObject the object that owns the property.
    @nestedPropertyPath the path to the nested property.
    */
    function getNestedPropertyValue(sourceObject, nestedPropertyPath) {
        var propertyChain = nestedPropertyPath.split(".");
        var value = sourceObject;

        for (var i = 0, len = propertyChain.length; i < len; i++) {
            var property = propertyChain[i];
            value = value[property];
            if (value === undefined) {
                throw "Object does not have property '" + property + "'";
            }
        }
        return value;
    }

    /*
    Sets the sourceObject nested property value.
    Example:    a = { b: { c : "" } };
    setNestedPropertyValue(a, "b.c", "hi") changes object a to { b: { c : "hi" } }.
    @sourceObject the object that owns the property.
    @nestedPropertyPath the path to the nested property.
    @value the value to set.
    */
    function setNestedPropertyValue(sourceObject, nestedPropertyPath, value) {
        if (!sourceObject) {
            throw "Cannot set property of an empty object";
        }
        if (!nestedPropertyPath) {
            throw "Cannot set property with empty name";
        }

        var propertyOwner;
        var propertyToSet;
        var nextOwner = sourceObject;
        var propertyChain = nestedPropertyPath.split(".");

        for (var i = 0, len = propertyChain.length; i < len; i++) {
            var property = propertyChain[i];

            if (nextOwner[property] === undefined) {
                nextOwner[property] = {};
            }
            propertyOwner = nextOwner;
            propertyToSet = property;
            nextOwner = nextOwner[property];
        }
        ;
        propertyOwner[propertyToSet] = value;
    }

    /*
    Returns true, if object is array, otherwise, false.
    */
    var isArray = Function["isArray"] || function (obj) {
        return typeof obj === "object" && Object.prototype.toString.call(obj) === "[object Array]";
    };

    /*
    Returns true, if object is function, otherwise, false.
    */
    function isFunction(obj) {
        return Object.prototype.toString.call(obj) === "[object Function]";
    }

    /*
    Generates a new guid.
    */
    function generateNewGuid() {
        var guid = "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g, function (c) {
            var rnd = Math.random() * 16 | 0;
            var c1 = (c == 'x') ? rnd : (rnd & 0x3 | 0x8);
            return c1.toString(16);
        });
        return guid;
    }

    // *****************************************************************************************
    // *                             Load Model                                                *
    // *****************************************************************************************
    /*
    Load and returns ui model.
    If model uri is not specified, returns empty model.
    */
    function loadModel() {
        var modelUrl = _reduiApplication.data(REDUI_APPLICATION_MODEL_DATA_NAME);

        if (!modelUrl) {
            return {};
        }

        try  {
            var model = {};

            $.ajax({
                url: modelUrl,
                dataType: "json",
                async: false,
                success: function (modelJson) {
                    model = modelJson;
                }
            });

            return model;
        } catch (e) {
            throw "Error while loading model: " + e;
        }
    }

    // *****************************************************************************************
    // *                             Control Creation                                          *
    // *****************************************************************************************
    /*
    Creates a new window with default properties.
    */
    function createNewWindow() {
        var window = {
            // Privates
            _allControls: {},
            _toHtml: toHtml,
            // Properties
            model: null,
            id: "redui_window_" + getNewId(),
            parent: null,
            window: null,
            type: REDUI_CONTROL_TYPE_WINDOW,
            name: "",
            controls: [],
            left: 0,
            top: 0,
            width: 0,
            height: 0,
            titleWidth: 0,
            titleHeight: 0,
            clientAreaWidth: 0,
            clientAreaHeight: 0,
            bindingContext: null,
            // Methods
            bind: bind,
            getElement: null,
            show: null,
            close: null,
            showModal: null,
            dock: null,
            undock: null,
            closing: function (handler) {
                this.getElement().bind(REDUI_EVENT_WINDOW_CLOSING, handler);
            },
            closed: function (handler) {
                this.getElement().bind(REDUI_EVENT_WINDOW_CLOSED, handler);
            }
        };
        return window;
    }

    /*
    Creates a new group box with default properties.
    */
    function createNewGroupBox() {
        var groupBox = {
            // Properties
            model: null,
            id: "redui_control_" + getNewId(),
            parent: null,
            window: null,
            type: REDUI_CONTROL_TYPE_GROUPBOX,
            name: "",
            controls: [],
            bindingContext: null,
            // Methods
            bind: bind,
            getElement: null,
            _toHtml: toHtml
        };
        return groupBox;
    }

    /*
    Creates a new text box with default properties.
    */
    function createNewTextBox() {
        var textBox = {
            // Properties
            model: null,
            id: "redui_control_" + getNewId(),
            name: "",
            type: REDUI_CONTROL_TYPE_TEXTBOX,
            parent: null,
            window: null,
            bindingContext: null,
            // Methods
            bind: bind,
            getElement: null,
            _toHtml: toHtml
        };
        return textBox;
    }

    /*
    Creates a new text area with default properties.
    */
    function createNewTextArea() {
        var textArea = {
            // Properties
            model: null,
            id: "redui_control_" + getNewId(),
            name: "",
            type: REDUI_CONTROL_TYPE_TEXTAREA,
            parent: null,
            window: null,
            bindingContext: null,
            // Methods
            bind: bind,
            getElement: null,
            _toHtml: toHtml
        };
        return textArea;
    }

    /*
    Creates a new check box with default properties.
    */
    function createNewCheckBox() {
        var checkBox = {
            // Properties
            model: null,
            id: "redui_control_" + getNewId(),
            name: "",
            type: REDUI_CONTROL_TYPE_CHECKBOX,
            parent: null,
            window: null,
            bindingContext: null,
            // Methods
            bind: bind,
            getElement: null,
            _toHtml: toHtml
        };
        return checkBox;
    }

    /*
    Creates a new combo box with default properties.
    */
    function createNewComboBox() {
        var comboBox = {
            // Properties
            model: null,
            id: "redui_control_" + getNewId(),
            name: "",
            type: REDUI_CONTROL_TYPE_COMBOBOX,
            parent: null,
            window: null,
            options: null,
            bindingContext: null,
            // Methods
            bind: bind,
            getElement: null,
            _toHtml: toHtml
        };
        return comboBox;
    }

    /*
    Creates a new button with default properties.
    */
    function createNewButton() {
        var button = {
            // Properties
            model: null,
            id: "redui_control_" + getNewId(),
            name: "",
            type: REDUI_CONTROL_TYPE_BUTTON,
            parent: null,
            window: null,
            // Methods
            getElement: null,
            _toHtml: toHtml
        };
        return button;
    }

    /*
    Creates a new grid view with default properties.
    */
    function createNewGridView() {
        var gridView = {
            // Properties
            model: null,
            id: "redui_control_" + getNewId(),
            name: "",
            type: REDUI_CONTROL_TYPE_GRIDVIEW,
            parent: null,
            window: null,
            columns: [],
            rows: [],
            currentRow: null,
            bindingContext: null,
            // Methods
            bind: bind,
            getElement: null,
            _toHtml: toHtml,
            createNewRowValue: null,
            rowSelected: function (handler) {
                this.getElement().bind(REDUI_EVENT_GRIDVIEW_ROWSELECTED, handler);
            }
        };
        return gridView;
    }

    /*
    Creates a new grid view row with default properties.
    */
    function createNewGridViewRow() {
        var gridViewRow = {
            // Properties
            model: null,
            id: "redui_control_" + getNewId(),
            name: "",
            type: REDUI_CONTROL_TYPE_GRIDVIEWROW,
            parent: null,
            window: null,
            cells: [],
            isSentinel: false,
            bindingContext: null,
            // Methods
            bind: bind,
            getElement: null,
            _toHtml: toHtml
        };
        return gridViewRow;
    }

    /*
    Creates a new grid view column with default properties.
    @columnType the column type.
    */
    function createNewGridViewColumn(columnType) {
        var gridViewColumn = {
            // Properties
            model: null,
            id: "redui_control_" + getNewId(),
            name: "",
            type: columnType,
            parent: null,
            window: null,
            // Methods
            getElement: null,
            _toHtml: toHtml
        };
        return gridViewColumn;
    }

    /*
    Creates a new grid view combobox column with default properties.
    */
    function createNewGridViewComboBoxColumn() {
        var gridViewColumn = createNewGridViewColumn(REDUI_CONTROL_TYPE_GRIDVIEWCOMBOBOXCOLUMN);
        gridViewColumn.options = null;
        return gridViewColumn;
    }

    /*
    Creates a new grid view cell with default properties.
    @cellType the cell type.
    */
    function createNewGridViewCell(cellType) {
        var gridViewCell = {
            // Properties
            model: null,
            id: "redui_control_" + getNewId(),
            name: "",
            type: cellType,
            parent: null,
            window: null,
            row: null,
            value: "",
            bindingContext: null,
            // Methods
            bind: bind,
            getElement: null,
            _toHtml: toHtml
        };
        return gridViewCell;
    }

    /*
    Creates a new grid view combobox cell with default properties.
    */
    function createNewGridViewComboBoxCell() {
        var gridViewCell = createNewGridViewCell(REDUI_CONTROL_TYPE_GRIDVIEWCOMBOBOXCELL);
        gridViewCell.options = [];
        return gridViewCell;
    }

    /*
    Creates a new tree view with default properties.
    */
    function createNewTreeView() {
        var treeView = {
            // Properties
            model: null,
            id: "redui_control_" + getNewId(),
            name: "",
            type: REDUI_CONTROL_TYPE_TREEVIEW,
            parent: null,
            window: null,
            roots: null,
            currentNode: null,
            bindingContext: null,
            // Methods
            bind: bind,
            getElement: null,
            _toHtml: toHtml,
            nodeSelected: function (handler) {
                this.getElement().bind(REDUI_EVENT_TREEVIEW_NODESELECTED, handler);
            },
            nodeExpanded: function (handler) {
                this.getElement().bind(REDUI_EVENT_TREEVIEWNODE_EXPANDED, handler);
            }
        };
        return treeView;
    }

    /*
    Creates a new tree view node with default properties.
    */
    function createNewTreeViewNode() {
        var treeViewNode = {
            // Properties
            model: null,
            id: "redui_control_" + getNewId(),
            name: "",
            type: REDUI_CONTROL_TYPE_TREEVIEWNODE,
            controls: [],
            parent: null,
            window: null,
            children: [],
            treeView: null,
            bindingContext: null,
            // Methods
            bind: bind,
            getElement: null,
            _toHtml: toHtml
        };
        return treeViewNode;
    }

    /*
    Creates a new static text with default properties.
    */
    function createNewStaticText() {
        var staticText = {
            // Properties
            model: null,
            id: "redui_control_" + getNewId(),
            name: "",
            type: REDUI_CONTROL_TYPE_STATICTEXT,
            parent: null,
            window: null,
            bindingContext: null,
            // Methods
            bind: bind,
            getElement: null,
            _toHtml: toHtml
        };
        return staticText;
    }

    /*
    Creates a new image with default properties.
    */
    function createNewImage() {
        var image = {
            // Properties
            model: null,
            id: "redui_control_" + getNewId(),
            name: "",
            type: REDUI_CONTROL_TYPE_IMAGE,
            parent: null,
            window: null,
            bindingContext: null,
            // Methods
            bind: bind,
            getElement: null,
            _toHtml: toHtml
        };
        return image;
    }

    /*
    Creates a new object selector with default properties.
    */
    function createNewObjectSelector() {
        var objectSelector = {
            // Properties
            model: null,
            id: "redui_control_" + getNewId(),
            name: "",
            type: REDUI_CONTROL_TYPE_OBJECTSELECTOR,
            parent: null,
            window: null,
            bindingContext: null,
            // Methods
            bind: bind,
            getElement: null,
            _toHtml: toHtml
        };
        return objectSelector;
    }

    /*
    Creates a new pager with default properties.
    */
    function createNewPager() {
        var pager = {
            // Properties
            model: null,
            id: "redui_control_" + getNewId(),
            name: "",
            type: REDUI_CONTROL_TYPE_PAGER,
            parent: null,
            window: null,
            bindingContext: null,
            // Methods
            bind: bind,
            getElement: null,
            _toHtml: toHtml,
            pageChanged: function (handler) {
                this.getElement().bind(REDUI_EVENT_PAGER_PAGECHANGED, handler);
            }
        };
        return pager;
    }

    // *****************************************************************************************
    // *                             Control Initialization                                    *
    // *****************************************************************************************
    /*
    Initializes a new window from the model.
    @reduiWindow window to initialize.
    @windowName the window name.
    */
    function initializeWindowFromModel(reduiWindow) {
        // Find a window definition with a given name
        var windowModel = findInArray(_model.windows, function (x) {
            return x.name === reduiWindow.name;
        });
        if (!windowModel) {
            throw "Window with name '" + reduiWindow.name + "' is not found in the model.";
        }
        reduiWindow.model = windowModel;

        // Normalize model
        windowModel.isHidden = true;
        if (!windowModel.title) {
            windowModel.title = "";
        }
        if (!windowModel.width) {
            windowModel.width = 0;
        }
        if (!windowModel.height) {
            windowModel.height = 0;
        }
        if (!windowModel.left) {
            windowModel.left = 0;
        }
        if (!windowModel.top) {
            windowModel.top = 0;
        }

        // Initialize window own properties
        reduiWindow.width = windowModel.width;
        reduiWindow.height = windowModel.height;
        reduiWindow.left = windowModel.left;
        reduiWindow.top = windowModel.top;

        reduiWindow.titleWidth = reduiWindow.width;
        reduiWindow.titleHeight = REDUI_WINDOW_TITLE_HEIGHT;

        reduiWindow.clientAreaWidth = reduiWindow.width;
        reduiWindow.clientAreaHeight = reduiWindow.height - REDUI_WINDOW_TITLE_HEIGHT;

        // Initialize methods
        reduiWindow.getElement = function () {
            return $("#" + reduiWindow.id);
        };

        if (windowModel.controls) {
            initializeControls(reduiWindow, reduiWindow, windowModel.controls);
        }
    }

    /*
    Initializes parent control inner controls.
    @window owner window.
    @parentControl parent control
    @controlsModel model that is used to initialize controls.
    */
    function initializeControls(window, parentControl, controlsModel) {
        for (var i = 0, len = controlsModel.length; i < len; i++) {
            var controlModel = controlsModel[i];

            // Create a new control
            var newControl = initializeControlFromModel(window, controlModel);

            // Make parent accessible
            newControl.parent = parentControl;

            // Make window accessible
            newControl.window = window;

            // Make control accessible by name
            parentControl[newControl.name] = newControl;

            // Make control element accessible
            newControl.getElement = (function (id) {
                return function () {
                    return $("#" + id);
                };
            })(newControl.id);

            // Add control to the collection of the parent.
            parentControl.controls.push(newControl);

            // Add control to the collection of the window.
            window._allControls[newControl.id] = newControl;
        }
        ;
    }

    /*
    Initializes a control from a model.
    @window owner window.
    @controlModel the model that describes a control.
    */
    function initializeControlFromModel(window, controlModel) {
        if (!controlModel.type) {
            throw "Control type is not specified in the model (" + JSON.stringify(controlModel) + ")";
        }
        if (!controlModel.name) {
            controlModel.name = "generatedName";
        }
        if (!controlModel.cssClass) {
            controlModel.cssClass = "";
        }
        if (controlModel.isHidden) {
            controlModel.isHidden = true;
        } else {
            controlModel.isHidden = false;
        }

        var newControl;
        switch (controlModel.type) {
            case REDUI_CONTROL_TYPE_TEXTBOX:
                newControl = createNewTextBox();
                initializeTextBoxFromModel(newControl, controlModel);
                break;
            case REDUI_CONTROL_TYPE_TEXTAREA:
                newControl = createNewTextArea();
                initializeTextAreaFromModel(newControl, controlModel);
                break;
            case REDUI_CONTROL_TYPE_CHECKBOX:
                newControl = createNewCheckBox();
                initializeCheckBoxFromModel(newControl, controlModel);
                break;
            case REDUI_CONTROL_TYPE_GROUPBOX:
                newControl = createNewGroupBox();
                initializeGroupBoxFromModel(window, newControl, controlModel);
                break;
            case REDUI_CONTROL_TYPE_COMBOBOX:
                newControl = createNewComboBox();
                initializeComboBoxFromModel(newControl, controlModel);
                break;
            case REDUI_CONTROL_TYPE_BUTTON:
                newControl = createNewButton();
                initializeButtonFromModel(newControl, controlModel);
                break;
            case REDUI_CONTROL_TYPE_GRIDVIEW:
                newControl = createNewGridView();
                initializeGridViewFromModel(window, newControl, controlModel);
                break;
            case REDUI_CONTROL_TYPE_GRIDVIEWTEXTBOXCOLUMN:
                newControl = createNewGridViewColumn(REDUI_CONTROL_TYPE_GRIDVIEWTEXTBOXCOLUMN);
                initializeGridViewColumnFromModel(newControl, controlModel);
                break;
            case REDUI_CONTROL_TYPE_GRIDVIEWCHECKBOXCOLUMN:
                newControl = createNewGridViewColumn(REDUI_CONTROL_TYPE_GRIDVIEWCHECKBOXCOLUMN);
                initializeGridViewColumnFromModel(newControl, controlModel);
                break;
            case REDUI_CONTROL_TYPE_GRIDVIEWCOMBOBOXCOLUMN:
                newControl = createNewGridViewComboBoxColumn();
                initializeGridViewComboBoxColumnFromModel(newControl, controlModel);
                break;
            case REDUI_CONTROL_TYPE_GRIDVIEWSTATICTEXTCOLUMN:
                newControl = createNewGridViewColumn(REDUI_CONTROL_TYPE_GRIDVIEWSTATICTEXTCOLUMN);
                initializeGridViewColumnFromModel(newControl, controlModel);
                break;
            case REDUI_CONTROL_TYPE_GRIDVIEWOBJECTSELECTORCOLUMN:
                newControl = createNewGridViewColumn(REDUI_CONTROL_TYPE_GRIDVIEWOBJECTSELECTORCOLUMN);
                initializeGridViewColumnFromModel(newControl, controlModel);
                break;
            case REDUI_CONTROL_TYPE_TREEVIEW:
                newControl = createNewTreeView();
                initializeTreeViewFromModel(newControl, controlModel);
                break;
            case REDUI_CONTROL_TYPE_STATICTEXT:
                newControl = createNewStaticText();
                initializeStaticTextFromModel(newControl, controlModel);
                break;
            case REDUI_CONTROL_TYPE_IMAGE:
                newControl = createNewImage();
                initializeImageFromModel(newControl, controlModel);
                break;
            case REDUI_CONTROL_TYPE_OBJECTSELECTOR:
                newControl = createNewObjectSelector();
                initializeObjectSelectorFromModel(newControl, controlModel);
                break;
            case REDUI_CONTROL_TYPE_PAGER:
                newControl = createNewPager();
                initializePagerFromModel(newControl, controlModel);
                break;
            default:
                throw "Cannot initialize control from model. Control type '" + controlModel.type + "' is not supported.";
        }
        newControl.model = controlModel;
        newControl.name = controlModel.name;
        return newControl;
    }

    /*
    Initializes a new textbox from the model.
    @newTextBox text box to initialize.
    @controlModel control model.
    */
    function initializeTextBoxFromModel(newTextBox, controlModel) {
        if (!controlModel.label) {
            controlModel.label = "";
        }
    }

    /*
    Initializes a new textarea from the model.
    @newTextArea text area to initialize.
    @controlModel control model.
    */
    function initializeTextAreaFromModel(newTextArea, controlModel) {
        if (!controlModel.label) {
            controlModel.label = "";
        }
        if (!controlModel.columns) {
            controlModel.columns = REDUI_TEXTAREA_COLUMNS_DEFAULT;
        }
        if (!controlModel.rows) {
            controlModel.rows = REDUI_TEXTAREA_ROWS_DEFAULT;
        }
    }

    /*
    Initializes a new checkbox from the model.
    @newCheckBox check box to initialize.
    @controlModel control model.
    */
    function initializeCheckBoxFromModel(newCheckBox, controlModel) {
        if (!controlModel.label) {
            controlModel.label = "";
        }
    }

    /*
    Initializes a new combobox from the model.
    @newComboBox combo box to initialize.
    @controlModel control model.
    */
    function initializeComboBoxFromModel(newComboBox, controlModel) {
        if (!controlModel.label) {
            controlModel.label = "";
        }

        var optionsMapped = getOptions(newComboBox, controlModel);
        newComboBox.options = optionsMapped;
    }

    /*
    Initializes a new button from the model.
    @newButton button to initialize.
    @controlModel control model.
    */
    function initializeButtonFromModel(newButton, controlModel) {
        if (!controlModel.text) {
            controlModel.text = "";
        }
        if (controlModel.isDialogButton) {
            controlModel.isDialogButton = true;
        } else {
            controlModel.isDialogButton = false;
        }
        if (!controlModel.dialogResult) {
            controlModel.dialogResult = "";
        }
    }

    /*
    Initializes a new group box from the model.
    @window owner window.
    @newGroupBox group box to initialize.
    @controlModel control model.
    */
    function initializeGroupBoxFromModel(window, newGroupBox, controlModel) {
        if (!controlModel.label) {
            controlModel.label = "";
        }
        initializeControls(window, newGroupBox, controlModel.controls);
    }

    /*
    Initializes a new grid view from the model.
    @window owner window.
    @newGridView grid view to initialize.
    @controlModel control model.
    */
    function initializeGridViewFromModel(window, newGridView, controlModel) {
        if (!controlModel.label) {
            controlModel.label = "";
        }
        if ("canUserAddRows" in controlModel) {
            if (controlModel.canUserAddRows) {
                controlModel.canUserAddRows = true;
            } else {
                controlModel.canUserAddRows = false;
            }
        } else {
            // Not set, enable by default
            controlModel.canUserAddRows = true;
        }

        newGridView.columns = [];
        if ("columns" in controlModel) {
            for (var i = 0, len = controlModel.columns.length; i < len; i++) {
                var columnModel = controlModel.columns[i];
                var column = initializeControlFromModel(window, columnModel);
                newGridView.columns.push(column);
            }
            ;
        }
    }

    /*
    Initializes a new column from the model.
    @newColumn column to initialize.
    @controlModel control model.
    */
    function initializeGridViewColumnFromModel(newColumn, controlModel) {
        if (!controlModel.label) {
            controlModel.label = "";
        }
        if (!controlModel.width) {
            controlModel.width = 0;
        }
    }

    /*
    Initializes a new combobox column from the model.
    @newColumn column to initialize.
    @controlModel control model.
    */
    function initializeGridViewComboBoxColumnFromModel(newColumn, controlModel) {
        initializeGridViewColumnFromModel(newColumn, controlModel);

        var optionsMapped = getOptions(newColumn, controlModel);
        newColumn.options = optionsMapped;
    }

    /*
    Initializes a grid view row from model.
    @gridViewRow grid view row to initialize.
    @gridView a grid view that row belongs to.
    @columns an array of column models.
    @rowValue the row value (the object that the row will databind to).
    */
    function initializeGridViewRow(gridViewRow, gridView, columnModels, rowValue) {
        // Create an empty row
        gridViewRow.parent = gridView;
        gridViewRow.getElement = function () {
            return $("#" + gridViewRow.id);
        };
        gridViewRow.bindingContext = rowValue;

        // Model
        gridViewRow.model = {
            bindingContext: ""
        };

        if (isArray(columnModels)) {
            for (var i = 0, len = columnModels.length; i < len; i++) {
                var columnModel = columnModels[i];

                var cellValue;
                if (rowValue) {
                    try  {
                        cellValue = getNestedPropertyValue(rowValue, columnModel.bindsTo);
                    } catch (e) {
                        throw "Error databinding cell for column '" + columnModel.label + "': Data object does not have property '" + columnModel.bindsTo + "'";
                    }
                }

                var gridViewCell;
                switch (columnModel.type) {
                    case REDUI_CONTROL_TYPE_GRIDVIEWTEXTBOXCOLUMN:
                        gridViewCell = createNewGridViewCell(REDUI_CONTROL_TYPE_GRIDVIEWTEXTBOXCELL);
                        break;
                    case REDUI_CONTROL_TYPE_GRIDVIEWCHECKBOXCOLUMN:
                        gridViewCell = createNewGridViewCell(REDUI_CONTROL_TYPE_GRIDVIEWCHECKBOXCELL);
                        if (!cellValue) {
                            cellValue = false;
                        }
                        break;
                    case REDUI_CONTROL_TYPE_GRIDVIEWCOMBOBOXCOLUMN:
                        var gridViewComboBoxCell = createNewGridViewComboBoxCell();

                        var column = findInArray(gridView.columns, function (x) {
                            return x.name === columnModel.name;
                        });

                        gridViewComboBoxCell.options = column.options;
                        gridViewCell = gridViewComboBoxCell;
                        break;
                    case REDUI_CONTROL_TYPE_GRIDVIEWSTATICTEXTCOLUMN:
                        gridViewCell = createNewGridViewCell(REDUI_CONTROL_TYPE_GRIDVIEWSTATICTEXTCELL);
                        break;
                    case REDUI_CONTROL_TYPE_GRIDVIEWOBJECTSELECTORCOLUMN:
                        gridViewCell = createNewGridViewCell(REDUI_CONTROL_TYPE_GRIDVIEWOBJECTSELECTORCELL);
                        cellValue = getObjectValuePropertyValue(columnModel, cellValue);
                        break;
                    default:
                        throw "Cannor initialize grid view row. Column type '" + columnModel.type + "' is not supported.";
                }

                gridViewCell.parent = gridViewRow;
                gridViewCell.row = gridViewRow;
                gridViewCell.value = cellValue;
                gridViewCell.getElement = (function (id) {
                    return function () {
                        return $("#" + id);
                    };
                })(gridViewCell.id);

                // Binding context
                gridViewCell.bindingContext = gridViewRow.bindingContext;

                // Model
                gridViewCell.model = columnModel;

                gridViewRow.cells.push(gridViewCell);

                gridViewRow.window = gridView.window;
                gridViewRow.window._allControls[gridViewCell.id] = gridViewCell;
            }
            ;
        }

        gridView.rows.push(gridViewRow);
    }

    /*
    Initializes a tree view from model.
    @newTreeView tree view to initialize.
    @controlModel control model.
    */
    function initializeTreeViewFromModel(newTreeView, controlModel) {
        if (!controlModel.label) {
            controlModel.label = "";
        }

        newTreeView.roots = [];
    }

    /*
    Initializes any tree view node from model, root or not root.
    Should not be called directly. Instead call initializeRootTreeViewNodeFromModel or initializeTreeViewNodeFromModel.
    @window owner window.
    @newTreeViewNode tree view node to initialize.
    @controlModel control model.
    */
    function initializeAnyTreeViewNodeFromModel(window, newTreeViewNode, controlModel) {
        newTreeViewNode.children = [];

        newTreeViewNode.getElement = function () {
            return $("#" + newTreeViewNode.id);
        };

        // Model
        newTreeViewNode.model = controlModel;

        newTreeViewNode.window = window;
        window._allControls[newTreeViewNode.id] = newTreeViewNode;

        initializeControls(window, newTreeViewNode, controlModel.controls);
    }

    /*
    Initializes root tree view node from model, root or not root.
    @window owner window.
    @newTreeViewNode tree view node to initialize.
    @treeView tree view the node belongs to.
    @controlModel control model.
    */
    function initializeRootTreeViewNodeFromModel(window, newTreeViewNode, treeView, controlModel) {
        initializeAnyTreeViewNodeFromModel(window, newTreeViewNode, controlModel);

        newTreeViewNode.parent = treeView;
        newTreeViewNode.treeView = treeView;

        treeView.roots.push(newTreeViewNode);
    }

    /*
    Initializes non-root tree view node from model, root or not root.
    @window owner window.
    @newTreeViewNode tree view node to initialize.
    @treeView parent node the node belongs to.
    @controlModel control model.
    */
    function initializeTreeViewNodeFromModel(window, newTreeViewNode, parentNode, controlModel) {
        initializeAnyTreeViewNodeFromModel(window, newTreeViewNode, controlModel);

        newTreeViewNode.parent = parentNode;
        newTreeViewNode.treeView = parentNode.treeView;

        parentNode.children.push(newTreeViewNode);
    }

    /*
    Initializes a new static text from the model.
    @newStaticText static text to initialize.
    @controlModel control model.
    */
    function initializeStaticTextFromModel(newStaticText, controlModel) {
        if (!controlModel.text) {
            controlModel.text = "";
        }
    }

    /*
    Initializes a new image from the model.
    @newImage image to initialize.
    @controlModel control model.
    */
    function initializeImageFromModel(newImage, controlModel) {
        if (!controlModel.source) {
            controlModel.source = "";
        }
        if (!controlModel.alternateText) {
            controlModel.alternateText = "";
        }
        if (!controlModel.width) {
            controlModel.width = 0;
        }
        if (!controlModel.height) {
            controlModel.height = 0;
        }
    }

    /*
    Initializes a new object selector from the model.
    @newObjectSelector object selector to initialize.
    @controlModel control model.
    */
    function initializeObjectSelectorFromModel(newObjectSelector, controlModel) {
        if (!controlModel.label) {
            controlModel.label = "";
        }
    }

    /*
    Initializes a new pager from the model.
    @newPager pager to initialize.
    @controlModel control model.
    */
    function initializePagerFromModel(newPager, controlModel) {
    }

    /*
    Retrieves the options for combobox from the function that is set on the combobox model.
    @control control to retrieve the options for.
    @controlModel control model.
    */
    function getOptions(control, controlModel) {
        var optionsMapped = [];
        if (controlModel.getOptionsFunction) {
            var options = [];
            if (isFunction(_optionsBag[controlModel.getOptionsFunction])) {
                options = _optionsBag[controlModel.getOptionsFunction]();
            } else {
                throw "Error retrieving options for control '" + control.name + "': Options bag does not have a function with name '" + controlModel.getOptionsFunction + "'";
            }
            if (!isArray(options)) {
                throw "Value returned by the function '" + controlModel.getOptionsFunction + "' used as an 'options' for control '" + control.name + "' is not an array.";
            }

            var optionIdProperty = controlModel.optionIdProperty;
            if (!optionIdProperty) {
                optionIdProperty = "id";
            }
            var optionValueProperty = controlModel.optionValueProperty;
            if (!optionValueProperty) {
                optionValueProperty = "value";
            }

            optionsMapped = $.map(options, function (option) {
                if (!(optionIdProperty in option)) {
                    throw "Option value bound to control '" + controlModel.name + "' doesn't have property '" + optionIdProperty + "'";
                }
                if (!(optionValueProperty in option)) {
                    throw "Option value bound to control '" + controlModel.name + "' doesn't have property '" + optionValueProperty + "'";
                }

                var optionMapped = {};
                optionMapped["id"] = option[optionIdProperty];
                optionMapped["value"] = option[optionValueProperty];
                return optionMapped;
            });
        }
        return optionsMapped;
    }

    /*
    Sets the object selector value.
    @objectSelector object selector.
    @object object to set.
    */
    function setObjectSelectorValue(objectSelector, object) {
        var objectSelectorModel = objectSelector.model;
        var objectValueProperty = objectSelectorModel.objectValueProperty;
        if (!objectValueProperty) {
            objectValueProperty = "value";
        }

        if (!(objectValueProperty in object)) {
            throw "Object bound to control '" + objectSelectorModel.name + "' doesn't have property '" + objectValueProperty + "'";
        }

        var element = $("#" + objectSelector.id);
        element.val(object[objectValueProperty]);
    }
    ;

    /*
    Gets the object selector value
    @objectSelectorModel object selector model
    @object cell value
    */
    function getObjectValuePropertyValue(objectSelectorModel, object) {
        var objectValueProperty = objectSelectorModel.objectValueProperty;
        if (!objectValueProperty) {
            objectValueProperty = "value";
        }

        if (!(objectValueProperty in object)) {
            throw "Object bound to control '" + objectSelectorModel.name + "' doesn't have property '" + objectValueProperty + "'";
        }

        return object[objectValueProperty];
    }

    // *****************************************************************************************
    // *                             Rendering                                                 *
    // *****************************************************************************************
    /*
    Returns the HTML for a given control.
    @reduiControl RedUI control.
    */
    function toHtml() {
        var reduiControl = this;

        var template = _templates[reduiControl.type];

        if (!template) {
            var templateUrl = _templatesBaseUrl + reduiControl.type + ".htm";
            var error;
            $.ajax({
                url: templateUrl,
                dataType: "html",
                async: false,
                success: function (templateHtm) {
                    template = Mustache.compile(templateHtm);
                    _templates[reduiControl.type] = template;
                },
                error: function () {
                    error = "Template for type '" + reduiControl.type + "'is not found by url: '" + templateUrl + "'";
                }
            });
            if (error) {
                throw error;
            }
        }

        return template(reduiControl);
    }

    // *****************************************************************************************
    // *                             DataBinding                                               *
    // *****************************************************************************************
    /* Bind the control property values to the dataobject property values
    @dataobject object with the data.
    */
    function bind(dataObject) {
        var control = this;
        bindRecursively(control, dataObject, control.model);
    }

    function bindRecursively(dataBoundControl, dataObject, controlModel) {
        // Determine the binding context: dataObject or a property inside the dataObject
        var containerControlModel = controlModel;
        if (containerControlModel.bindingContext) {
            try  {
                dataObject = getNestedPropertyValue(dataObject, containerControlModel.bindingContext);
            } catch (e) {
                throw "Databinding context cannot be set for control '" + dataBoundControl.name + "': Data object does not have property '" + containerControlModel.bindingContext + "'";
            }
        }

        // Save context object on the databound control
        dataBoundControl.bindingContext = dataObject;

        if (controlModel.bindsTo) {
            try  {
                var value = getNestedPropertyValue(dataObject, controlModel.bindsTo);
            } catch (e) {
                throw "Error databinding control '" + dataBoundControl.name + "': Data object does not have property '" + controlModel.bindsTo + "'";
            }

            var element = dataBoundControl.getElement();
            switch (dataBoundControl.type) {
                case REDUI_CONTROL_TYPE_TEXTBOX:
                    element.val(value);
                    break;
                case REDUI_CONTROL_TYPE_TEXTAREA:
                    element.val(value);
                    break;
                case REDUI_CONTROL_TYPE_CHECKBOX:
                    element.attr('checked', value);
                    break;
                case REDUI_CONTROL_TYPE_COMBOBOX:
                    element.val(value);
                    break;
                case REDUI_CONTROL_TYPE_GRIDVIEW:
                    if (!isArray(value)) {
                        throw "Value of the property '" + controlModel.bindsTo + "' bound to grid view '" + dataBoundControl.name + "' is not an array.";
                    }
                    var gridView = dataBoundControl;
                    var gridViewModel = controlModel;

                    if ("createNewRowValueFunction" in gridViewModel) {
                        try  {
                            gridView.createNewRowValue = getNestedPropertyValue(dataObject, gridViewModel.createNewRowValueFunction);
                        } catch (e) {
                            throw "Error accessing createNewRowValueFunction for control '" + dataBoundControl.name + "': Data object does not have function with name '" + gridViewModel.createNewRowValueFunction + "'";
                        }
                    } else {
                        if (gridViewModel.canUserAddRows) {
                            throw "Error initializing control '" + dataBoundControl.name + "': 'createNewRowValueFunction' is required in the model when 'canUserAddRows' is set to true.";
                        }
                    }

                    // Clear rows
                    gridView.rows = [];
                    $("." + REDUI_CLASS_GRIDVIEW_GRIDVIEWROW, element).remove();

                    // Append rows in a non-blocking fashion
                    // This is an id that used to sync 2 binding attempts
                    // This is in case bind was called before the previous async bind has finished
                    var syncId = generateNewGuid();

                    // Each grid view is bound individually, new binding call always wins
                    gridView["__syncId"] = syncId;
                    appendRows(gridView, value, 0, syncId, function () {
                        if (gridViewModel.canUserAddRows) {
                            var sentinelRow = createSentinelRow(gridView, gridViewModel.columns);
                            appendEventsToSentinelRow(sentinelRow, gridView, gridViewModel.columns);
                        }
                    });
                    break;
                case REDUI_CONTROL_TYPE_GRIDVIEWTEXTBOXCELL:
                    element.val(value);
                    break;
                case REDUI_CONTROL_TYPE_GRIDVIEWCHECKBOXCELL:
                    element.attr('checked', value);
                    break;
                case REDUI_CONTROL_TYPE_GRIDVIEWCOMBOBOXCELL:
                    element.val(value);
                    break;
                case REDUI_CONTROL_TYPE_GRIDVIEWSTATICTEXTCELL:
                    element.text(value);
                    break;
                case REDUI_CONTROL_TYPE_GRIDVIEWOBJECTSELECTORCELL:
                    setObjectSelectorValue(dataBoundControl, value);
                    break;
                case REDUI_CONTROL_TYPE_TREEVIEW:
                    if (!isArray(value)) {
                        throw "Value of the property '" + controlModel.bindsTo + "' bound to tree view '" + dataBoundControl.name + "' is not an array.";
                    }
                    var treeView = dataBoundControl;
                    var treeViewModel = controlModel;

                    // Clear roots
                    treeView.roots = [];
                    $("." + REDUI_CLASS_TREEVIEWNODE_OUTER, element).remove();

                    var html = "";
                    for (var i = 0, len = value.length; i < len; i++) {
                        var nodeValue = value[i];
                        var treeViewNode = createNewTreeViewNode();
                        initializeRootTreeViewNodeFromModel(dataBoundControl.window, treeViewNode, treeView, treeViewModel.nodeModel);
                        html += treeViewNode._toHtml();
                    }
                    ;

                    // Append all nodes at once
                    $(treeView.getElement()).append(html);

                    // Bind tree to data binding related events
                    bindToDataBoundControlsChanged(dataBoundControl.window, "#" + treeView.id + " ." + REDUI_CLASS_ONCHANGEUPDATESDATA);
                    bindToObjectSelectors(dataBoundControl.window, "#" + treeView.id + " ." + REDUI_CLASS_OBJECTSELECTOR);

                    for (var i = 0, rootsLen = treeView.roots.length; i < rootsLen; i++) {
                        bindChildTreeViewNode(treeView.roots[i], value[i]);
                    }
                    ;

                    break;
                case REDUI_CONTROL_TYPE_TREEVIEWNODE:
                    if (!isArray(value)) {
                        throw "Value of the property '" + controlModel.bindsTo + "' bound to tree view node'" + dataBoundControl.name + "' is not an array.";
                    }
                    var parentNode = dataBoundControl;
                    var treeViewNodeModel = controlModel;

                    // Clear clildren
                    parentNode.children = [];
                    $("." + REDUI_CLASS_TREEVIEWNODE_OUTER, element).remove();

                    var html = "";
                    for (var i = 0, len = value.length; i < len; i++) {
                        var nodeValue = value[i];
                        var treeViewNode = createNewTreeViewNode();
                        initializeTreeViewNodeFromModel(dataBoundControl.window, treeViewNode, parentNode, treeViewNodeModel);
                        html += treeViewNode._toHtml();
                    }
                    ;

                    // Append all nodes at once
                    $("#" + parentNode.id + "_innernodes").first().append(html);

                    // Bind tree to data binding related events
                    bindToDataBoundControlsChanged(dataBoundControl.window, "#" + parentNode.id + " > ." + REDUI_CLASS_TREEVIEWNODE_INNERNODES + " ." + REDUI_CLASS_ONCHANGEUPDATESDATA);
                    bindToObjectSelectors(dataBoundControl.window, "#" + parentNode.id + " > ." + REDUI_CLASS_TREEVIEWNODE_INNERNODES + " ." + REDUI_CLASS_OBJECTSELECTOR);

                    for (var i = 0, childrenLen = parentNode.children.length; i < childrenLen; i++) {
                        bindChildTreeViewNode(parentNode.children[i], value[i]);
                    }
                    ;

                    break;
                case REDUI_CONTROL_TYPE_STATICTEXT:
                    element.text(value);
                    break;
                case REDUI_CONTROL_TYPE_IMAGE:
                    element.attr("src", value);
                    break;
                case REDUI_CONTROL_TYPE_OBJECTSELECTOR:
                    setObjectSelectorValue(dataBoundControl, value);
                    break;
                case REDUI_CONTROL_TYPE_PAGER:
                    bindPagerToData(dataBoundControl, value);
                    break;
                default:
                    throw "Control type '" + dataBoundControl.type + "' does not support data binding.";
            }
        } else if (dataBoundControl.type === REDUI_CONTROL_TYPE_GRIDVIEWROW) {
            if ("cells" in dataBoundControl) {
                var gridVIewRow = dataBoundControl;
                for (var i = 0, cellsLen = gridVIewRow.cells.length; i < cellsLen; i++) {
                    var cell = gridVIewRow.cells[i];
                    bindRecursively(cell, dataObject, cell.model);
                }
                ;
            }
        }
        ;

        if ("controls" in dataBoundControl) {
            var containerControl = dataBoundControl;
            for (var i = 0, innerControlsLen = containerControl.controls.length; i < innerControlsLen; i++) {
                var innerControl = containerControl.controls[i];
                if (isFunction(innerControl["bind"])) {
                    bindRecursively(innerControl, dataObject, innerControl.model);
                }
            }
            ;
        }
    }

    /*
    Appends rows to the grid view in batches
    @gridView Grid view
    @rowValues Array of row values
    @startIndex Index of the first row of the current batch
    @callback Function to be called after all batches are processed
    @syncId id used to sync to parallel attempts to data bind
    */
    function appendRows(gridView, rowValues, startIndex, syncId, callback) {
        if (gridView["__syncId"] !== syncId)
            return;

        if (!_redui.windows[gridView.window.id])
            return;

        // Get to the model
        var gridViewModel = gridView.model;

        // Create and initialize rows
        var batchId = gridView.id + "_rowbatch_" + startIndex;
        var html = "<tbody id='" + batchId + "'>";
        var lastIndex = startIndex;
        var rowsTotal = rowValues.length;
        var createdRows = [];
        for (var i = startIndex, len = Math.min(startIndex + ROWS_TO_RENDER_IN_ONE_BATCH, rowsTotal); i < len; i++, lastIndex++) {
            var gridViewRow = createNewGridViewRow();
            initializeGridViewRow(gridViewRow, gridView, gridViewModel.columns, rowValues[i]);
            html += gridViewRow._toHtml();
            createdRows.push(gridViewRow);
        }
        ;
        html += "</tbody>";

        // Append all rows at once
        gridView.getElement().append(html);

        for (var i = 0, len = createdRows.length; i < len; i++) {
            attachOnClickHandlerToGridViewRow(createdRows[i]);
        }

        // Bind cells to data binding related events (Sentinel row will be bound individually)
        bindToDataBoundControlsChanged(gridView.window, "#" + batchId + " ." + REDUI_CLASS_ONCHANGEUPDATESDATA);
        bindToObjectSelectors(gridView.window, "#" + batchId + " ." + REDUI_CLASS_OBJECTSELECTORCELL);

        if (lastIndex < rowsTotal) {
            setTimeout(appendRows, 25, gridView, rowValues, lastIndex, syncId, callback);
        } else {
            if (callback) {
                callback();
            }
        }
    }

    function attachOnClickHandlerToGridViewRow(gridViewRow) {
        var rowElement = gridViewRow.getElement();
        rowElement.click(function () {
            var gridViewOwner = gridViewRow.parent;
            if (gridViewOwner.currentRow) {
                gridViewOwner.currentRow.getElement().removeClass(REDUI_CLASS_GRIDVIEW_GRIDVIEWROW_SELECTED);
            }
            gridViewOwner.currentRow = gridViewRow;
            rowElement.addClass(REDUI_CLASS_GRIDVIEW_GRIDVIEWROW_SELECTED);
            rowElement.trigger(REDUI_EVENT_GRIDVIEW_ROWSELECTED, gridViewRow);
        });
    }

    function createSentinelRow(gridView, columns) {
        var gridViewRow = createNewGridViewRow();
        initializeGridViewRow(gridViewRow, gridView, columns, gridView.createNewRowValue());
        gridViewRow.isSentinel = true;
        $("tbody:last", gridView.getElement()).append(gridViewRow._toHtml());
        attachOnClickHandlerToGridViewRow(gridViewRow);

        bindToDataBoundControlsChanged(gridView.window, "#" + gridViewRow.id + " ." + REDUI_CLASS_ONCHANGEUPDATESDATA);
        bindToObjectSelectors(gridView.window, "#" + gridViewRow.id + " ." + REDUI_CLASS_OBJECTSELECTORCELL);

        return gridViewRow;
    }

    function appendEventsToSentinelRow(gridViewRow, gridView, columns) {
        var element = gridViewRow.getElement();
        element.bind(REDUI_EVENT_CHANGE, function () {
            gridViewRow.isSentinel = false;
            element.unbind(REDUI_EVENT_CHANGE);
            element.removeClass(REDUI_CLASS_GRIDVIEW_SENTINELGRIDVIEWROW);

            // Update the data object
            var gridViewModel = gridView.model;
            if (gridView.bindingContext && gridViewModel.bindsTo) {
                gridView.bindingContext[gridViewModel.bindsTo].push(gridViewRow.bindingContext);
            }

            var newSentinelRow = createSentinelRow(gridView, columns);
            appendEventsToSentinelRow(newSentinelRow, gridView, columns);
        });
    }

    function bindChildTreeViewNode(treeViewNode, value) {
        treeViewNode.bind(value);

        var element = treeViewNode.getElement();

        if (treeViewNode.children.length > 0) {
            element.addClass(REDUI_CLASS_TREEVIEWNODE_COLLAPSED);

            // Broadcast events: expanded
            $("#" + treeViewNode.id + "_expander").click(function () {
                if (element.hasClass(REDUI_CLASS_TREEVIEWNODE_COLLAPSED)) {
                    element.trigger(REDUI_EVENT_TREEVIEWNODE_EXPANDED, treeViewNode);
                }
                element.toggleClass(REDUI_CLASS_TREEVIEWNODE_COLLAPSED);
                if (treeViewNode.children.length === 0) {
                    element.addClass(REDUI_CLASS_TREEVIEWNODE_EMPTY);
                }
            });
        } else {
            element.addClass(REDUI_CLASS_TREEVIEWNODE_EMPTY);
        }

        // Broadcast events: selected
        $("#" + treeViewNode.id + "_node").click(function () {
            var treeViewOwner = treeViewNode.treeView;
            if (treeViewOwner.currentNode) {
                treeViewOwner.currentNode.getElement().removeClass(REDUI_CLASS_TREEVIEWNODE_SELECTED);
            }
            treeViewOwner.currentNode = treeViewNode;
            var nodeElement = treeViewNode.getElement();
            nodeElement.addClass(REDUI_CLASS_TREEVIEWNODE_SELECTED);
            nodeElement.trigger(REDUI_EVENT_TREEVIEW_NODESELECTED, treeViewNode);
        });
    }

    /*
    Binds pager to the data
    @control pager control
    @value pager info
    */
    function bindPagerToData(control, value) {
        $("#" + control.id + "_firstpage").unbind(REDUI_EVENT_CLICK);
        $("#" + control.id + "_lastpage").unbind(REDUI_EVENT_CLICK);

        var controlModel = control.model;

        var pageNoProperty = controlModel.pageNoProperty;
        if (!pageNoProperty) {
            pageNoProperty = "pageNo";
        }
        var pagesTotalProperty = controlModel.pagesTotalProperty;
        if (!pagesTotalProperty) {
            pagesTotalProperty = "pagesTotal";
        }

        if (!(pageNoProperty in value)) {
            throw "Pager info value bound to control '" + controlModel.name + "' doesn't have property '" + pageNoProperty + "'";
        }
        if (!(pagesTotalProperty in value)) {
            throw "Pager info value bound to control '" + controlModel.name + "' doesn't have property '" + pagesTotalProperty + "'";
        }

        var pagerInfo = {
            pageNo: value[pageNoProperty],
            pagesTotal: value[pagesTotalProperty]
        };

        renderPager(control, pagerInfo.pageNo, pagerInfo.pagesTotal);

        $("#" + control.id + "_firstpage").bind(REDUI_EVENT_CLICK, function () {
            $("#" + control.id).trigger(REDUI_EVENT_PAGER_PAGECHANGED, 1);
        });
        $("#" + control.id + "_lastpage").bind(REDUI_EVENT_CLICK, function () {
            $("#" + control.id).trigger(REDUI_EVENT_PAGER_PAGECHANGED, pagerInfo.pagesTotal);
        });

        $("#" + control.id + " .redui-pager-page").bind(REDUI_EVENT_CLICK, function () {
            $("#" + control.id).trigger(REDUI_EVENT_PAGER_PAGECHANGED, +$(this).data("pageno"));
        });
    }

    /*
    Renders pager
    @control pager control
    @pageNo current page, counting from 1
    @pagesTotal total number of pages
    */
    function renderPager(control, pageNo, pagesTotal) {
        // Const
        var visiblePositions = 6;

        if (pagesTotal < 1) {
            return;
        }

        if (pageNo < 1 || pageNo > pagesTotal) {
            return;
        }

        var pagesDiv = $("#" + control.id + "_pages");
        pagesDiv.empty();

        // First visible page
        var firstVisiblePageNo = pageNo - (visiblePositions >> 1) + 1;

        if (firstVisiblePageNo < 1) {
            firstVisiblePageNo = 1;
        }

        // Number of pages in sequence
        var lastPageNoInSequence = firstVisiblePageNo + visiblePositions - 1;

        if (lastPageNoInSequence > pagesTotal) {
            lastPageNoInSequence = pagesTotal;

            if (lastPageNoInSequence - firstVisiblePageNo + 1 < visiblePositions) {
                firstVisiblePageNo = lastPageNoInSequence - visiblePositions + 1;
            }

            if (firstVisiblePageNo < 1) {
                firstVisiblePageNo = 1;
            }
        }

        var showEllipsis = false;
        if (lastPageNoInSequence < pagesTotal) {
            lastPageNoInSequence -= 2;
            showEllipsis = true;
        }

        for (var renderedPageNo = firstVisiblePageNo; renderedPageNo <= lastPageNoInSequence; renderedPageNo++) {
            renderPage(pagesDiv, renderedPageNo, pageNo);
        }

        if (showEllipsis) {
            renderEllipsis(pagesDiv);
            renderPage(pagesDiv, pagesTotal, pageNo);
        }
    }

    /*
    Renders page
    @renderedPageNo number of the page to render
    @pageNo current page, counting from 1
    */
    function renderPage(pagesDiv, renderedPageNo, pageNo) {
        if (renderedPageNo === pageNo) {
            pagesDiv.append("<button class='redui-pager-button redui-focusable redui-pager-page redui-pager-currentpage' tabindex='0' type='button' data-pageno='" + renderedPageNo + "'>" + renderedPageNo + "</button>");
        } else {
            pagesDiv.append("<button class='redui-pager-button redui-focusable redui-pager-page' tabindex='0' type='button' data-pageno='" + renderedPageNo + "'>" + renderedPageNo + "</button>");
        }
    }

    /*
    Renders ellipsis
    */
    function renderEllipsis(pagesDiv) {
        pagesDiv.append("<span class='redui-pager-ellipsis'>...</span>");
    }

    /*
    Updates the data bound object with the new value
    @dataBoundControl a databound control
    */
    function updateBinding(dataBoundControl) {
        var controlModel = dataBoundControl.model;
        var dataObject = dataBoundControl.bindingContext;

        if (dataObject && controlModel.bindsTo) {
            var element = dataBoundControl.getElement();
            var value = null;
            switch (dataBoundControl.type) {
                case REDUI_CONTROL_TYPE_TEXTBOX:
                    value = element.val();
                    break;
                case REDUI_CONTROL_TYPE_TEXTAREA:
                    value = element.val();
                    break;
                case REDUI_CONTROL_TYPE_COMBOBOX:
                    value = element.val();
                    break;
                case REDUI_CONTROL_TYPE_CHECKBOX:
                    value = element.is(':checked');
                    break;
                case REDUI_CONTROL_TYPE_GRIDVIEWTEXTBOXCELL:
                    value = element.val();
                    break;
                case REDUI_CONTROL_TYPE_GRIDVIEWCHECKBOXCELL:
                    value = element.is(':checked');
                    break;
                case REDUI_CONTROL_TYPE_GRIDVIEWCOMBOBOXCELL:
                    value = element.val();
                    break;
                default:
                    throw "Cannot update data object. Control type '" + dataBoundControl.type + "' does not support data binding.";
            }
            setNestedPropertyValue(dataObject, controlModel.bindsTo, value);
        }
    }

    // *****************************************************************************************
    // *                             Events                                                    *
    // *****************************************************************************************
    /*
    Binds to changed events for input controls
    @newWindow the window the control belongs to
    @selector the selector used to find all input controls in HTML
    */
    function bindToDataBoundControlsChanged(newWindow, selector) {
        var clientArea = $("#" + newWindow.id + "_clientareainner");
        $(selector, clientArea).each(function (index, element) {
            var id = $(element).attr("id");
            var dataBoundControl = newWindow._allControls[id];
            var controlModel = dataBoundControl.model;

            if (controlModel.bindsTo) {
                var updateControlBinding = (function (control) {
                    return function () {
                        updateBinding(control);
                    };
                })(dataBoundControl);

                dataBoundControl.getElement().bind(REDUI_EVENT_CHANGE, updateControlBinding);
            }
        });
    }

    /*
    Binds to object selection events for object selectors
    @newWindow the window the object selector belongs to
    @selector the selector used to find all object selectors in HTML
    */
    function bindToObjectSelectors(newWindow, selector) {
        var clientArea = $("#" + newWindow.id + "_clientareainner");
        $(selector, clientArea).each(function (index, element) {
            var id = $(element).attr("id");
            var selectButton = $("#" + id + "_lookupbutton", clientArea);
            $(selectButton).click(function (event) {
                var objectSelector = newWindow._allControls[id];
                var objectSelectorModel = objectSelector.model;

                if (objectSelectorModel.getObjectFunction) {
                    if (isFunction(_objectSources[objectSelectorModel.getObjectFunction])) {
                        _objectSources[objectSelectorModel.getObjectFunction](function callback(object) {
                            // Update the value shown in UI
                            setObjectSelectorValue(objectSelector, object);

                            // Update the databound object
                            var dataObject = objectSelector.bindingContext;
                            if (dataObject && objectSelectorModel.bindsTo) {
                                setNestedPropertyValue(dataObject, objectSelectorModel.bindsTo, object);
                            }

                            $(element).trigger(REDUI_EVENT_CHANGE, objectSelector);
                        });
                    } else {
                        throw "Error retrieving data object for control '" + objectSelector.name + "': Object sources does not have a function with name '" + objectSelectorModel.getObjectFunction + "'";
                    }
                }
            });
        });
    }

    /*
    Binds to click events for dialog buttons
    @newWindow the window the button belongs to
    */
    function bindToDialogButtons(newWindow) {
        $("." + REDUI_CLASS_DIALOGBUTTON, newWindow.getElement()).each(function (index, element) {
            var id = $(element).attr("id");
            var button = newWindow._allControls[id];
            var buttonModel = button.model;

            if (buttonModel.dialogResult) {
                var closeWithResult = (function (dialogResult) {
                    return function () {
                        newWindow.close(dialogResult);
                    };
                })(buttonModel.dialogResult);

                $(element).click(closeWithResult);
            }
        });
    }

    // *****************************************************************************************
    // *                             RedUI object                                              *
    // *****************************************************************************************
    var _redui = {
        // Application UI model
        model: _model,
        // Application windows
        windows: {},
        // Creates a new window
        createNewWindow: null,
        // Options bag
        optionsBag: _optionsBag,
        // Object sources
        objectSources: _objectSources
    };

    /*
    Creates a new window.
    @windowClass - the type of the window
    */
    _redui.createNewWindow = function (windowName) {
        if (!windowName) {
            throw "Window name is not provided.";
        }
        if (!_redui.model) {
            throw "UI model is not set. Make sure you specified the data-model attribute of the RedUI application (<div id='redui_application' ... data-model='...' ... />)";
        }
        if (!("windows" in _model)) {
            throw "Property 'windows' is not found in the model.";
        }
        if (!isArray(_model.windows)) {
            throw "Property 'windows' is not an array.";
        }

        var redui = this;

        // New window object with default values.
        var newWindow = createNewWindow();
        newWindow.name = windowName;

        initializeWindowFromModel(newWindow);

        newWindow.show = function () {
            var windowElement = newWindow.getElement();

            windowElement.removeClass(REDUI_CLASS_HIDDEN);

            //$("#" + this.id, $(REDUI_APPLICATION_ID)).dialog(); TODO: optional?
            // TODO: should be optional
            windowElement.draggable({
                containment: REDUI_APPLICATION_ID,
                handle: "#" + newWindow.id + "_titleouter"
            });
            // TODO: should be optional?
            // TODO: resize all inner controls accordingly
            // $("#" + this.id, $(REDUI_APPLICATION_ID)).resizable();
        };

        newWindow.showModal = function () {
            var windowElement = newWindow.getElement();

            if (!windowElement.hasClass(REDUI_CLASS_MODALWINDOW)) {
                var depth = modalDepthCounter.increase();

                windowElement.css("zIndex", 1000 + depth * 2);
                $("#" + this.id + "_overlay").css("zIndex", 1000 + depth * 2 - 1);

                windowElement.addClass(REDUI_CLASS_MODALWINDOW);
                $("#" + this.id + "_overlay").removeClass(REDUI_CLASS_HIDDEN);
            }

            // Center window
            windowElement.css({
                margin: "-" + newWindow.height / 2 + "px 0 0 -" + newWindow.width / 2 + "px",
                top: "50%",
                left: "50%"
            });

            newWindow.show();

            var firstFocusable = $("." + REDUI_CLASS_FOCUSABLE + ":first:not(." + REDUI_CLASS_HIDDEN + ")", windowElement);
            var lastFocusable = $("." + REDUI_CLASS_FOCUSABLE + ":last:not(." + REDUI_CLASS_HIDDEN + ")", windowElement);

            // Move the focus to the first control inside the modal window
            firstFocusable.focus();
            firstFocusable.focus();

            // Prevent focus to leave the modal dialog
            windowElement.keydown(function (e) {
                if (e.keyCode === 9) {
                    if (e.target === lastFocusable[0] && !e.shiftKey) {
                        firstFocusable.focus();
                        e.preventDefault();
                    } else if (e.target === firstFocusable[0] && e.shiftKey) {
                        lastFocusable.focus();
                        e.preventDefault();
                    }
                }
            });
        };

        newWindow.dock = function (divSelector) {
            if (!divSelector.is("div")) {
                throw "Cannot dock a window. Selector '" + divSelector + "' does not return div";
            }

            var windowElement = newWindow.getElement();

            windowElement.addClass(REDUI_CLASS_HIDDEN);

            var innerClientArea = $("#" + newWindow.id + "_clientareainner");
            divSelector.append(innerClientArea);

            newWindow.undock = function () {
                newWindow.show();

                var outerClientArea = $("#" + newWindow.id + "_clientareaouter");
                outerClientArea.append(innerClientArea);

                newWindow.undock = function () {
                };
            };
        };

        newWindow.undock = function () {
        };

        newWindow.close = function (dialogResult) {
            var windowElement = newWindow.getElement();

            windowElement.trigger(REDUI_EVENT_WINDOW_CLOSING, dialogResult);

            if (windowElement.hasClass(REDUI_CLASS_MODALWINDOW)) {
                windowElement.css("zIndex", "");
                windowElement.removeClass(REDUI_CLASS_MODALWINDOW);
                var depth = modalDepthCounter.decrease();
            }

            $("#" + this.id + "_overlay").remove();
            $("#" + this.id + "_dialogactions").remove();

            windowElement.trigger(REDUI_EVENT_WINDOW_CLOSED, dialogResult);

            // Delete window from DOM
            windowElement.remove();

            // Delete window from the application window dictionary
            delete redui.windows[newWindow.id];

            // Move focus into the previously opened modal dialog
            var firstFocusable = $("." + REDUI_CLASS_MODALWINDOW + ":last ." + REDUI_CLASS_FOCUSABLE + ":first:not(." + REDUI_CLASS_HIDDEN + ")");
            if (firstFocusable.length > 0) {
                firstFocusable.focus();
            }
        };

        // Adds the new window to the application windows
        this.windows[newWindow.id] = newWindow;

        // Create a new window html representation
        _reduiApplication.append(newWindow._toHtml());

        // Events
        // This will bind to events of all controls that are rendered upon window creation
        // Binding to controls added upon data binding (grid view rows & cells, tree nodes) should be done after data binding
        bindToDialogButtons(newWindow);
        bindToDataBoundControlsChanged(newWindow, "." + REDUI_CLASS_ONCHANGEUDPATESDATA);
        bindToObjectSelectors(newWindow, "." + REDUI_CLASS_OBJECTSELECTOR);

        return newWindow;
    };

    // Put redui object into the window property
    window["redui"] = _redui;
})(window);
//# sourceMappingURL=redui-1.0.0.js.map
