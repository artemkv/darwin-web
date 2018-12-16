{
    "windows": [
        {
            "name": "ProjectGridView",
            "title": "Projects",
            "left": 50,
            "top": 50,
            "width": 400,
            "height": 400,
            "controls": [
                {
                    "name": "ProjectGridView",
                    "label": "Projects",
                    "type": "gridview",
                    "bindsTo": "items",
                    "canUserAddRows": false,
                    "columns": [
                        {
                            "type": "gridviewstatictextcolumn",
                            "name": "NameColumn",
                            "label": "Name",
                            "width": 360,
                            "bindsTo": "projectName"
                        }
                    ]
                },
                {
                    "name": "OKButton",
                    "type": "button",
                    "text": "OK"
                },
                {
                    "name": "CancelButton",
                    "type": "button",
                    "text": "Cancel"
                }
            ]
        },
        {
            "name": "ProjectTreeView",
            "title": "ProjectTree",
            "left": 50,
            "top": 50,
            "width": 400,
            "height": 400,
            "controls": [
                {
                    "name": "ProjectTreeView",
                    "type": "treeview",
                    "bindsTo": "items",
                    "nodeModel": {
                        "name": "ProjectTreeNode",
                        "type": "treeviewnode",
                        "bindsTo": "items",
                        "controls": [
                            {
                                "name": "NodeIcon",
                                "type": "image",
                                "alternateText": "Icon",
                                "width": 16,
                                "height": 16,
                                "bindsTo": "iconUrl"
                            },
                            {
                                "name": "TitleStaticText",
                                "type": "statictext",
                                "text": "",
                                "bindsTo": "title"
                            }
                        ]
                    }
                }
            ]
        },
        {
            "name": "ProjectDetailsView",
            "title": "Project",
            "left": 50,
            "top": 50,
            "width": 400,
            "height": 400,
            "controls": [
                {
                    "name": "ProjectNameTextBox",
                    "type": "textbox",
                    "label": "Project Name",
                    "bindsTo": "projectName"
                }
            ]
        },
        {
            "name": "DatabaseDetailsView",
            "title": "Database",
            "left": 50,
            "top": 50,
            "width": 400,
            "height": 400,
            "controls": [
                {
                    "name": "DatabaseNameTextBox",
                    "type": "textbox",
                    "label": "Database Name",
                    "bindsTo": "dbname"
                },
                {
                    "name": "DataTypesGridView",
                    "label": "Data Types",
                    "type": "gridview",
                    "bindsTo": "dataTypes",
					"createNewRowValueFunction": "createNewRowValue",
                    "columns": [
                        {
                            "type": "gridviewtextboxcolumn",
                            "name": "NameColumn",
                            "label": "Name",
                            "width": 300,
                            "bindsTo": "typeName"
                        },
                        {
                            "type": "gridviewcheckboxcolumn",
                            "name": "HasLengthColumn",
                            "label": "Has Length",
                            "width": 100,
                            "bindsTo": "hasLength"
                        },
                        {
                            "type": "gridviewcomboboxcolumn",
                            "name": "BaseEnumColumn",
                            "label": "Base Enum",
                            "width": 200,
                            "bindsTo": "baseEnumId",
                            "getOptionsFunction": "getBaseEnums",
                            "optionIdProperty": "id",
                            "optionValueProperty": "baseEnumName"
                        }
                    ]
                }
            ]
        },
        {
            "name": "EnumDetailsView",
            "title": "Base Enum",
            "left": 50,
            "top": 50,
            "width": 400,
            "height": 400,
            "controls": [
                {
                    "name": "BaseEnumNameTextBox",
                    "type": "textbox",
                    "label": "Base Enum Name",
                    "bindsTo": "baseEnumName"
                },
                {
                    "name": "BaseEnumValuesGridView",
                    "label": "Values",
                    "type": "gridview",
                    "bindsTo": "values",
					"createNewRowValueFunction": "createNewRowValue",
                    "columns": [
                        {
                            "type": "gridviewtextboxcolumn",
                            "name": "NameColumn",
                            "label": "Name",
                            "width": 300,
                            "bindsTo": "name"
                        },
                        {
                            "type": "gridviewtextboxcolumn",
                            "name": "ValueColumn",
                            "label": "Value",
                            "width": 100,
                            "bindsTo": "value"
                        }
                    ]
                }
            ]
        },
        {
            "name": "EnumValueDetailsView",
            "title": "Base Enum Value",
            "left": 50,
            "top": 50,
            "width": 400,
            "height": 400,
            "controls": [
                {
                    "name": "BaseEnumValueNameTextBox",
                    "type": "textbox",
                    "label": "Name",
                    "bindsTo": "name"
                },
				{
                    "name": "BaseEnumValueValueTextBox",
                    "type": "textbox",
                    "label": "Value",
                    "bindsTo": "value"
                }
            ]
        },
        {
            "name": "DataTypeDetailsView",
            "title": "Data Type",
            "left": 50,
            "top": 50,
            "width": 400,
            "height": 400,
            "controls": [
                {
                    "name": "DataTypeNameTextBox",
                    "type": "textbox",
                    "label": "Type Name",
                    "bindsTo": "typeName"
                },
				{
                    "name": "HasLengthCheckBox",
                    "type": "checkbox",
                    "label": "Has Length",
                    "bindsTo": "hasLength"
                },
				{
                    "type": "BaseEnumComboBox",
                    "type": "combobox",
                    "label": "Base Enum",
                    "bindsTo": "baseEnumId",
                    "getOptionsFunction": "getBaseEnums",
                    "optionIdProperty": "id",
                    "optionValueProperty": "baseEnumName"
				}
            ]
        },
        {
            "name": "EntityDetailsView",
            "title": "Entity",
            "left": 50,
            "top": 50,
            "width": 400,
            "height": 400,
            "controls": [
                {
                    "name": "EntityNameTextBox",
                    "type": "textbox",
                    "label": "Entity Name",
                    "bindsTo": "entityName"
                },
                {
                    "name": "SchemaNameTextBox",
                    "type": "textbox",
                    "label": "Schema Name",
                    "bindsTo": "schemaName"
                },
                {
                    "name": "AttributesGridView",
                    "label": "Attributes",
                    "type": "gridview",
                    "bindsTo": "attributes",
					"createNewRowValueFunction": "createNewRowValue",
                    "columns": [
                        {
                            "type": "gridviewcheckboxcolumn",
                            "name": "PkColumn",
                            "label": "PK",
                            "width": 50,
                            "bindsTo": "isPrimaryKey"
                        },
                        {
                            "type": "gridviewtextboxcolumn",
                            "name": "NameColumn",
                            "label": "Name",
                            "width": 300,
                            "bindsTo": "attributeName"
                        },
                        {
                            "type": "gridviewcomboboxcolumn",
                            "name": "DataTypeColumn",
                            "label": "Type",
                            "width": 200,
                            "bindsTo": "dataTypeId",
							"getOptionsFunction": "getDataTypes",
                            "optionIdProperty": "id",
                            "optionValueProperty": "typeName"
                        },
                        {
                            "type": "gridviewtextboxcolumn",
                            "name": "LengthColumn",
                            "label": "Length",
                            "width": 100,
                            "bindsTo": "length"
                        },
                        {
                            "type": "gridviewcheckboxcolumn",
                            "name": "NotNullColumn",
                            "label": "Not Null",
                            "width": 100,
                            "bindsTo": "isRequired"
                        }
                    ]
                }
            ]
        },
        {
            "name": "AttributeDetailsView",
            "title": "Attribute",
            "left": 50,
            "top": 50,
            "width": 400,
            "height": 400,
            "controls": [
                {
                    "name": "PrimaryKeyCheckBox",
                    "type": "checkbox",
                    "label": "Primary Key",
                    "bindsTo": "isPrimaryKey"
                },
				{
                    "name": "DataTypeNameTextBox",
                    "type": "textbox",
                    "label": "Attribute Name",
                    "bindsTo": "attributeName"
                },
				{
                    "name": "AttributeTypeComboBox",
                    "type": "combobox",
                    "label": "Type",
                    "bindsTo": "dataTypeId",
					"getOptionsFunction": "getDataTypes",
                    "optionIdProperty": "id",
                    "optionValueProperty": "typeName"
				},
				{
                    "name": "LengthTextBox",
                    "type": "textbox",
                    "label": "Length",
                    "bindsTo": "length"
                },
				{
                    "name": "NotNullCheckBox",
                    "type": "checkbox",
                    "label": "Not Null",
                    "bindsTo": "isRequired"
                }
            ]
        },
        {
            "name": "RelationDetailsView",
            "title": "Relation",
            "left": 50,
            "top": 50,
            "width": 400,
            "height": 400,
            "controls": [
                {
                    "name": "RelationNameTextBox",
                    "type": "textbox",
                    "label": "Relation Name",
                    "bindsTo": "relationName"
                },
				{
				    "name": "parentEntityObjectSelector",
				    "type": "objectselector",
				    "label": "Parent Entity",
				    "bindsTo": "primaryEntity",
				    "getObjectFunction": "getEntity",
				    "objectValueProperty": "entitySchemaPrefixedName"
				},
				{
				    "name": "foreignEntityObjectSelector",
				    "type": "objectselector",
				    "label": "Foreign Entity",
				    "bindsTo": "foreignEntity",
				    "getObjectFunction": "getEntity",
				    "objectValueProperty": "entitySchemaPrefixedName"
				},
				{
				    "name": "ModalityComboBox",
				    "type": "combobox",
				    "label": "Modality",
				    "bindsTo": "atLeastOne",
				    "getOptionsFunction": "getModality"
				},
				{
				    "name": "CardinalityComboBox",
				    "type": "combobox",
				    "label": "Cardinality",
				    "bindsTo": "oneToOne",
				    "getOptionsFunction": "getCardinality"
				}
            ]
        },
        {
            "name": "EntitiesGridView",
            "title": "Entities",
            "width": 600,
            "height": 500,
            "controls": [
                {
                    "name": "EntitiesGridView",
                    "label": "Entities",
                    "type": "gridview",
                    "bindsTo": "items",
                    "canUserAddRows": false,
                    "columns": [
                        {
                            "type": "gridviewstatictextcolumn",
                            "name": "entityName",
                            "label": "Entity Name",
                            "width": 500,
                            "bindsTo": "entitySchemaPrefixedName"
                        }
                    ]
                },
                {
                    "name": "pager",
                    "type": "pager",
                    "bindsTo": "pagingInfo"
                },
                {
                    "name": "OKButton",
                    "type": "button",
                    "text": "OK",
                    "isDialogButton": true,
                    "dialogResult": "OK"
                },
                {
                    "name": "CancelButton",
                    "type": "button",
                    "text": "Cancel",
                    "isDialogButton": true,
                    "dialogResult": "Cancel"
                }
            ]
        }
    ]
}