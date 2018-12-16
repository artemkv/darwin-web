$(function () {
    "use strict";

    // Constants
    var PAGE_SIZE = 10;

    // Relations
    var RELATION_TYPE_SELF = "self";
    var RELATION_TYPE_GETNODES = "/rels/getprojectnodes";
    var RELATION_TYPE_DATABASES = "/rels/databases";
    var RELATION_TYPE_DATATYPES = "/rels/datatypes";
    var RELATION_TYPE_BASEENUMS = "/rels/baseenums";
    var RELATION_TYPE_ENTITIES = "/rels/entities";
    var RELATION_TYPE_ATTRIBUTES = "/rels/attributes";
    var RELATION_TYPE_RELATIONS = "/rels/relations";
    var RELATION_TYPE_RELATIONITEMS = "/rels/relationitems";
    var RELATION_TYPE_DIAGRAMS = "/rels/diagrams";
    var RELATION_TYPE_BASEENUMVALUES = "/rels/baseenumvalues";
    var RELATION_TYPE_BASEENUM = "/rels/baseenum";
    var RELATION_TYPE_DATATYPE = "/rels/datatype"
    var RELATION_TYPE_PRIMARYENTITY = "/rels/primaryentity";
    var RELATION_TYPE_PRIMARYATTRIBUTE = "/rels/primaryattribute";

    // Extracted collections
    var options = {};
    
    var entitiesUri = null;

    // Gets url by relation type
    function getUrl(obj, relationType) {
        var url = null;

        if (obj.links) {
            obj.links.forEach(function (link) {
                if (!url) {
                    if (link.rel && link.href) {
                        if (link.rel.length >= relationType.length &&
                            link.rel.substr(link.rel.length - relationType.length, relationType.length) === relationType) {
                            url = link.href;
                        }
                    }
                }
            });
        }
        return url;
    }

    // This node is required to make a node look like expandable before the subnodes are loaded
    var stubNode = {
        iconUrl: "", // TODO: check that this doesn't result in an extra call
        title: "stub",
        items: []
    };

    // Returns tree nodes
    function getTreeNodes(url) {
        var treeData = {};

        $.ajax({
            headers: {
                Accept: "application/json,*/*;q=0.0"
            },
            type: "POST",
            url: url,
            data: {
                pageSize: 100, // TODO: paging in the tree
                pageNumber: 0
            },
            async: false,
            success: function (data) {
                treeData = data;

                treeData.items.forEach(function (item) {
                    // Fill subnodes
                    item.items = [];
                    if (!item.isLeaf) {
                        item.items.push(stubNode);
                    }
                    // Link to load next nodes
                    item.next = getUrl(item, RELATION_TYPE_GETNODES);

                    // Type-specific logic
                    switch (item.objectType) {
                        case "Project":
                            break;
                        case "Database":
                            // Fill base enums
                            options.baseEnums = [];
                            options.baseEnums.push({ id: "00000000-0000-0000-0000-000000000000", baseEnumName: "" });
                            options.baseEnums = options.baseEnums.concat(item.database.baseEnums.slice(0));
                            // Fill data types
                            options.dataTypes = [];
                            options.dataTypes.push({ id: "00000000-0000-0000-0000-000000000000", typeName: "" });
                            options.dataTypes = options.dataTypes.concat(item.database.dataTypes.slice(0));
                            // Remember Uri to retrieve entities
                            entitiesUri = getUrl(item.database, RELATION_TYPE_ENTITIES);
                            break;
                        case "":
                            break;
                        case "BaseEnum":
                            break;
                        case "BaseEnumValue":
                            break;
                        case "DataType":
                            break;
                        case "Entity":
                            break;
                        case "Attribute":
                            break;
                        case "Relation":
                            break;
                        case "RelationItem":
                            break;
                        default:
                            item.iconUrl = "";
                            break;
                    }
                });
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert(textStatus + "|" + errorThrown);
            }
        });

        return treeData;
    }

    // Option bags - base enums
    redui.optionsBag.getBaseEnums = function () {
        return options.baseEnums;
    }

    // Option bags - data types
    redui.optionsBag.getDataTypes = function () {
        return options.dataTypes;
    }

    // Option bags - modality
    redui.optionsBag.getModality = function () {
        return [
            {
                id: "false",
                value: "Zero Or"
            },
            {
                id: "true",
                value: "One Or"
            }
        ];
    }

    // Option bags - modality
    redui.optionsBag.getCardinality = function () {
        return [
            {
                id: "false",
                value: "Many"
            },
            {
                id: "true",
                value: "One"
            }
        ];
    }

    function getEntities(pageNo) {
        var entities = null;
        if (entitiesUri) { // TODO: what if not?
            $.ajax({
                headers: {
                    Accept: "application/json,*/*;q=0.0"
                },
                type: "GET",
                url: entitiesUri,
                data: {
                    pageSize: PAGE_SIZE,
                    pageNumber: pageNo - 1
                },
                async: false,
                success: function (data) {
                    entities = data;
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert(textStatus + "|" + errorThrown);
                }
            });

            var pagesTotal = Math.ceil(entities.count / PAGE_SIZE);
            entities.pagingInfo = {
                pageNo: pageNo,
                pagesTotal: pagesTotal
            };
        }
        return entities;
    }

    redui.objectSources.getEntity = function (callback) {
        var entities = getEntities(1);

        var entitiesGridViewWindow = redui.createNewWindow("EntitiesGridView");
        entitiesGridViewWindow.bind(entities);
        entitiesGridViewWindow.showModal();

        entitiesGridViewWindow.pager.pageChanged(function (target, pageNo) {
            var entities = getEntities(pageNo);
            entitiesGridViewWindow.bind(entities);
        });

        entitiesGridViewWindow.closed(function (target, dialogResult) {
            if (dialogResult === "OK") {
                var object = entitiesGridViewWindow.EntitiesGridView.currentRow.bindingContext;
                callback(object);
            }
        });
    };

    // Project selector
    var projectGridViewWindow = redui.createNewWindow("ProjectGridView");

    var projectsData = {};

    $.ajax({
        headers: {
            Accept: "application/json,*/*;q=0.0"
        },
        type: "GET",
        url: "projects",
        data: {
            pageSize: 20,
            pageNumber: 0
        },
        async: false,
        success: function (data) {
            projectsData = data;
        }
    });

    projectGridViewWindow.bind(projectsData);
//    projectGridViewWindow.show(); // TODO: Here we should select the project, but now we will continue with the first one

    var project = projectsData.items[0]; // TODO: hardcoded, need to get selected one


    // Start building the tree
    var treeNodesUrl = getUrl(project, RELATION_TYPE_GETNODES);
    var treeData = getTreeNodes(treeNodesUrl);
    var projectTreeViewWindow = redui.createNewWindow("ProjectTreeView");
    projectTreeViewWindow.bind(treeData);
    projectTreeViewWindow.dock($("#project_tree_inner"));

    projectTreeViewWindow.ProjectTreeView.nodeExpanded(function (target, node) {
        var nodeData = node.bindingContext;
        // TODO: nodeData.title != "Diagrams" is a temporary fix until we get diagrams in the scope
        if (!nodeData.isLeaf && nodeData.title != "Diagrams") {
            var nextLevelData = getTreeNodes(nodeData.next);
            nodeData.items = nextLevelData.items;
            node.bind(nodeData);
        }
    });

    // Reference to the details window (right pane)
    var detailsWindow;

    // Register to the grid events
    projectTreeViewWindow.ProjectTreeView.nodeSelected(function (target, node) {
        // Clear existing details window
        if (detailsWindow) {
            detailsWindow.undock();
            detailsWindow.close();
        }

        // Get the selected node data
        var details = node.bindingContext;

        // Get to the object
        // And create a new details window
        var object = null;
        if (details.objectType === "Project") {
            object = details.project;
            detailsWindow = redui.createNewWindow("ProjectDetailsView");
        }
        else if (details.objectType === "Database") {
            object = details.database;
            object.createNewRowValue = function () {
                return {
                    id: "",
                    ts: "",
                    baseEnumId: "00000000-0000-0000-0000-000000000000",
                    databaseId: "",
                    hasLength: false,
                    typeName: ""
                };
            }
            detailsWindow = redui.createNewWindow("DatabaseDetailsView");
        }
        else if (details.objectType === "BaseEnum") {
            detailsWindow = redui.createNewWindow("EnumDetailsView");
            object = details.baseEnum;
            object.createNewRowValue = function () {
                var dataObject = detailsWindow.BaseEnumValuesGridView.bindingContext;
                var max = -1;
                var length = dataObject.values.length;
                for (var i = 0; i < length; i++) {
                    var currentValue = parseInt(dataObject.values[i].value, 10);
                    if (currentValue > max) {
                        max = currentValue;
                    }
                }

                return {
                    id: "",
                    ts: "",
                    name: "",
                    value: ++max
                };
            }
        }
        else if (details.objectType === "BaseEnumValue") {
            object = details.baseEnumValue;
            detailsWindow = redui.createNewWindow("EnumValueDetailsView");
        }
        else if (details.objectType === "DataType") {
                object = details.dataType;
                detailsWindow = redui.createNewWindow("DataTypeDetailsView");
        }
        else if (details.objectType === "Entity") {
            object = details.entity;
            object.createNewRowValue = function () {
                return {
                    id: "",
                    ts: "",
                    attributeName: "",
                    dataTypeId: "00000000-0000-0000-0000-000000000000",
                    entityId: "",
                    isPrimaryKey: false,
                    isRequired: false,
                    length: 0
                };
            }
            detailsWindow = redui.createNewWindow("EntityDetailsView");
        }
        else if (details.objectType === "Attribute") {
            object = details.attribute;
            detailsWindow = redui.createNewWindow("AttributeDetailsView");
        }
        else if (details.objectType === "Relation") {
            object = details.relation;
            detailsWindow = redui.createNewWindow("RelationDetailsView");
        }

        if (object) {
            // Bind the details window to the selected details
            detailsWindow.bind(object);

            // Show the details window
            detailsWindow.dock($("#details_view_inner"));
        }
    });

});