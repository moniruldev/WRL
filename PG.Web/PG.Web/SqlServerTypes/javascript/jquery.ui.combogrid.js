/*
 * jQuery UI Combogrid 1.6.3
 *
 * Copyright 2011 D'Alia Sara
 * Dual licensed under the MIT or GPL Version 2 licenses.
 * http://jquery.org/license
 *
 * 
 * Depends:
 *	jquery.ui.core.js
 *	jquery.ui.widget.js
 *	jquery.ui.position.js
 *	jquery.i18n.properties.js
 */
(function ($, undefined) {

	$.widget("cg.combogrid", {
		options: {
			resetButton: false,
			resetFields: null,
			searchButton: false,
			searchIcon: false,
			okIcon: false,
			alternate: false,
			appendTo: "body",
			autoFocus: false,
			showError: false,
			autoChoose: false,
			delayChoose: 100,
			delay: 150,
			rows: 20,
			title: 'Select',
			showPager: true,
			addClass: null,
			addId: null,
			minLength: 0,
			munit: "%",
			position: {
				my: "left top",
				at: "left bottom",
				offset: "0 1",
				collision: "flip"
			},
			cache: false,
			async: true,
			url: null,
			colModel: null,
			sidx: "",
			sord: "",
			datatype: "json",
			debug: false,
			i18n: false,
			draggable: false,
			rememberDrag: false,
			replaceNull: false,
			rowsArray: [10, 20, 30, 'all'],
			showOn: false,
			scrollBar: false,
			scrollHeight: 120,
			height: null,
			width: null
		},
		source: null,
		lastOrdered: "",
		cssCol: "",
		pending: 0,
		page: 1,
		rowNumber: 0,
		//actualRecords: 0,
		errorNo: 0,
		errorString: '',
		dataterm: '',
		pos: null,
		_create: function () {
			var self = this,
			doc = this.element[0].ownerDocument,
			suppressKeyPress;
			if (self.options.resetButton) {
				this.element.after('<span class="ui-state-default ui-corner-all ' + self.element.attr('id') + ' cg-resetButton"><span class="ui-icon ui-icon-circle-close"></span></span>');
				$('.' + self.element.attr('id') + '.cg-resetButton').bind('click.combogrid', function () {
					self.element.val('');
					self.term = self.element.val();
					if (self.options.okIcon) {
						$('.' + self.element.attr('id') + '.ok-icon').remove();
						$('.' + self.element.attr('id') + '.notok-icon').remove();
						if (self.options.resetButton) {
							$('.' + self.element.attr('id') + '.cg-resetButton').after('<span class="' + self.element.attr('id') + ' notok-icon"></span>');
						} else if (self.options.searchButton) {
							$('.' + self.element.attr('id') + '.cg-searchButton').after('<span class="' + self.element.attr('id') + ' notok-icon"></span>');
						} else {
							self.element.after('<span class="' + self.element.attr('id') + ' notok-icon"></span>');
						}
					}
					//keyup trigger leaved for backward compatibility
					self.element.trigger('keyup');
					if (self.options.resetFields != null) {
						$.each(self.options.resetFields, function () {
							$('' + this).val('');
						});
					}
				});
			}
			if (self.options.searchButton) {
				this.element.after('<span class="ui-state-default ui-corner-all ' + self.element.attr('id') + ' cg-searchButton"><span class="ui-icon ui-icon-search"></span></span>');
				$('.' + self.element.attr('id') + '.cg-searchButton').bind('click.combogrid', function () {
					self.element.trigger('focus.combogrid');
					self._search(self.element.val());
					self.element.trigger('focus.combogrid');
				});
			}
			if (self.options.showOn) {
				this.element.focus(function () {
					self._search(self.element.val());
				});
			}
			this.element
			.addClass("ui-autocomplete-input")
			.attr("autocomplete", "off")
			// TODO verify these actually work as intended
			.attr({
				role: "textbox",
				"aria-autocomplete": "list",
				"aria-haspopup": "true"
			})
			.bind("keydown.combogrid", function (event) {
				if (self.options.disabled || self.element.attr("readonly")) {
					return;
				}

				suppressKeyPress = false;
				var keyCode = $.ui.keyCode;
				switch (event.keyCode) {
					case keyCode.LEFT:
						$('.' + self.element.attr('id') + '.cg-keynav-prev').trigger('click.combogrid');
						break;
					case keyCode.PAGE_UP:
						self._move("previousPage", event);
						break;
					case keyCode.RIGHT:
						$('.' + self.element.attr('id') + '.cg-keynav-next').trigger('click.combogrid');
						break;
					case keyCode.PAGE_DOWN:
						self._move("nextPage", event);
						break;
					case keyCode.UP:
						self._move("previous", event);
						// prevent moving cursor to beginning of text field in some browsers
						event.preventDefault();
						break;
					case keyCode.DOWN:
						self._move("next", event);
						// prevent moving cursor to end of text field in some browsers
						event.preventDefault();
						break;
					case keyCode.ENTER:
					case keyCode.NUMPAD_ENTER:
						// when menu is open and has focus
						if (self.menucombo.active) {
							// #6055 - Opera still allows the keypress to occur
							// which causes forms to submit
							suppressKeyPress = true;
							event.preventDefault();
							event.stopImmediatePropagation();
						}
						//passthrough - ENTER and TAB both select the current element
					case keyCode.TAB:
						if (!self.menucombo.active) {
							return;
						}
						self.menucombo.select(event);
						break;
					case keyCode.DELETE:
						if (self.options.okIcon) {
							$('.' + self.element.attr('id') + '.ok-icon').remove();
							$('.' + self.element.attr('id') + '.notok-icon').remove();
							if (self.options.resetButton) {
								$('.' + self.element.attr('id') + '.cg-resetButton').after('<span class="' + self.element.attr('id') + ' notok-icon"></span>');
							} else if (self.options.searchButton) {
								$('.' + self.element.attr('id') + '.cg-searchButton').after('<span class="' + self.element.attr('id') + ' notok-icon"></span>');
							} else {
								self.element.after('<span class="' + self.element.attr('id') + ' notok-icon"></span>');
							}
						}
						if (self.options.resetFields != null) {
							$.each(self.options.resetFields, function () {
								$('' + this).val('');
							});
						}
						//self.element.val('');
						//Introduced in 1.5.1 to trigger search on DELETE input field
						/*	// keypress is triggered before the input value is changed
						clearTimeout( self.searching );
					
						self.searching = setTimeout(function() {
						// only search if the value has changed
						if ( self.term != self.element.val()) {
						self.selectedItem = null;
						self.search( null, event );
						}
						}, self.options.delay );*/
						break;
					case keyCode.ESCAPE:


						if ($(self.menucombo.element).is(":visible")) {
							self.close(event);

						}


						//			            self.element.val(self.term);
						//			            self.close(event);
						//			            //ESCAPE needs this workaround
						//			            $('.' + self.element.attr('id') + '.ok-icon').remove();
						//			            $('.' + self.element.attr('id') + '.notok-icon').remove();
						//			            if (self.options.okIcon) {
						//			                if (self.options.resetButton) {
						//			                    $('.' + self.element.attr('id') + '.cg-resetButton').after('<span class="' + self.element.attr('id') + ' ok-icon"></span>');
						//			                } else if (self.options.searchButton) {
						//			                    $('.' + self.element.attr('id') + '.cg-searchButton').after('<span class="' + self.element.attr('id') + ' ok-icon"></span>');
						//			                } else {
						//			                    self.element.after('<span class="' + self.element.attr('id') + ' ok-icon"></span>');
						//			                }
						//			            }

						event.preventDefault();
						break;
					default:
						if (self.options.okIcon) {
							$('.' + self.element.attr('id') + '.ok-icon').remove();
							$('.' + self.element.attr('id') + '.notok-icon').remove();
							if (self.options.resetButton) {
								$('.' + self.element.attr('id') + '.cg-resetButton').after('<span class="' + self.element.attr('id') + ' notok-icon"></span>');
							} else if (self.options.searchButton) {
								$('.' + self.element.attr('id') + '.cg-searchButton').after('<span class="' + self.element.attr('id') + ' notok-icon"></span>');
							} else {
								self.element.after('<span class="' + self.element.attr('id') + ' notok-icon"></span>');
							}
						}
						// keypress is triggered before the input value is changed
						clearTimeout(self.searching);

						self.searching = setTimeout(function () {
							// only search if the value has changed
							//	if ( self.term != self.element.val()) {
							self.selectedItem = null;
							self.search(null, event);
							//	}
						}, self.options.delay);

						break;
				}
			})
			.bind("keypress.combogrid", function (event) {
				if (suppressKeyPress) {
					suppressKeyPress = false;
					event.preventDefault();
				}
			})
			.bind("focus.combogrid", function () {
				if (self.options.disabled) {
					return;
				}
				self.selectedItem = null;
				self.previous = self.element.val();
			})
			.bind("blur.combogrid", function (event) {
				if (self.options.disabled) {
					return;
				}

				if (this.cancelBlur) {
					delete this.cancelBlur;
					return;
				}


				//preventing from closing when a button trigger a search 
				if (self.options.searchButton) {
					if (self.menucombo.element.is(":visible")) {
						clearTimeout(self.searching);
						self.closing = setTimeout(function () {
							self.close(event);
							self._change(event);
						}, 70);
					}
				} else {
					clearTimeout(self.searching);
					// clicks on the menu (or a button to trigger a search) will cause a blur event
					self.closing = setTimeout(function () {
						self.close(event);
						self._change(event);
					}, 150);
				}
			});
			if (this.options.searchIcon) {
				this.element.addClass("input-bg");
			}
			this.options.source = function (request, response) {
				this.dataterm = request.term;
				$.ajax({
				    cache: self.options.cache,
				    async: self.options.async,
					url: self.options.url,
					dataType: self.options.datatype,
					data: {
						sidx: self.options.sidx,
						page: self.page,
						sord: self.options.sord,
						rows: self.options.rows,
						searchTerm: request.term
					},
					success: function (data) {
						//self.actualRecords = data.actualrecords;
						self.errorNo = data.errorno;
						self.errorString = data.errorstring;

						if (data.records == 0) {
							//                            self.pending--;
							//                            if (!self.pending) {
							//                                self.element.removeClass("cg-loading");
							//                                self.close();
							//                            }

							if (self.options.showError) {
								response(data.records, data.totalpage, $.map(data.rows, function (item) {
									return item;
								}));
							}
							else {
								self.pending--;
								if (!self.pending) {
									self.element.removeClass("cg-loading");
									self.close();
								}
							}

						} else if (data.records == 1) {

							response(data.records, data.totalpage, $.map(data.rows, function (item) {
								return item;
							}));
							//self.menucombo.activate($.Event({ type: "mouseenter" }), self.menucombo.element.children(".cg-menu-item:first"));
							self.menucombo.activate($.Event({ type: "mouseenter" }), self.menucombo.element.find(".cg-menu-item:first"));
							if (self.options.autoChoose) {
								setTimeout(function () {
									self.menucombo._trigger("selected", $.Event({ type: "click" }), { item: self.menucombo.active });
								}, self.options.delayChoose);
							}
						} else {
							response(data.records, data.totalpage, $.map(data.rows, function (item) {
								return item;
							}));
						}
					}
				});
			};
			this._initSource();
			this.response = function () {
				return self._response.apply(self, arguments);
			};
			this.menucombo = $("<div></div>")
			.addClass("cg-autocomplete")
			.appendTo($(this.options.appendTo || "body", doc)[0])
			// prevent the close-on-blur in case of a "slow" click on the menu (long mousedown)
			.mousedown(function (event) {
				// clicking on the scrollbar causes focus to shift to the body
				// but we can't detect a mouseup or a click immediately afterward
				// so we have to track the next mousedown and close the menu if
				// the user clicks somewhere outside of the autocomplete

				//event.preventDefault();

				var menuElement = self.menucombo.element[0];
				if (!$(event.target).closest(".cg-menu-item").length) {
					setTimeout(function () {
						$(document).one('mousedown', function (event) {
							if (event.target !== self.element[0] &&
								event.target !== menuElement &&
								!$.ui.contains(menuElement, event.target)) {
								self.close();
							}
						});
					}, 1);
				}

				// use another timeout to make sure the blur-event-handler on the input was already triggered
				setTimeout(function () {
					clearTimeout(self.closing);
				}, 13);
			})
			.menucombo({
				scrollBar: this.options.scrollBar,
				focus: function (event, ui) {
					var item = ui.item.data("item.combogrid");
					if (false !== self._trigger("focus", event, { item: item })) {
						// use value to match what will end up in the input, if it was a key event
						if (/^key/.test(event.originalEvent.type)) {
							//			                if (item.value != undefined)
							//			                    self.element.val(item.value);
							if (item != undefined) {
								if (item.value != undefined)
									self.element.val(item.value);
							}

						}
					}
				},
				selected: function (event, ui) {
					var item = ui.item.data("item.combogrid"),
						previous = self.previous;

					// only trigger when focus was lost (click on menu)
					if (self.element[0] !== doc.activeElement) {
						if (!self.options.showOn) {
							self.element.focus();
						}
						self.previous = previous;
						// #6109 - IE triggers two focus events and the second
						// is asynchronous, so we need to reset the previous
						// term synchronously and asynchronously :-(
						setTimeout(function () {
							self.previous = previous;
							self.selectedItem = item;
						}, 1);
					}

					if (false !== self._trigger("select", event, { item: item })) {
						self.element.val(item.value);
					}
					// reset the term after the select event
					// this allows custom select handling to work properly
					self.term = self.element.val();

					self.close(event);
					self.selectedItem = item;
					if (self.options.okIcon) {
						$('.' + self.element.attr('id') + '.ok-icon').remove();
						$('.' + self.element.attr('id') + '.notok-icon').remove();
						if (self.options.resetButton) {
							$('.' + self.element.attr('id') + '.cg-resetButton').after('<span class="' + self.element.attr('id') + ' ok-icon"></span>');
						} else if (self.options.searchButton) {
							$('.' + self.element.attr('id') + '.cg-searchButton').after('<span class="' + self.element.attr('id') + ' ok-icon"></span>');
						} else {
							self.element.after('<span class="' + self.element.attr('id') + ' ok-icon"></span>');
						}
					}
				},
				blur: function (event, ui) {
					// don't set the value of the text field if it's already correct
					// this prevents moving the cursor unnecessarily
					if (self.menucombo.element.is(":visible") &&
						(self.element.val() !== self.term)) {
						//	self.element.val( self.term );
					}
				}
			})
			.zIndex(this.element.zIndex() + 1)
			// workaround for jQuery bug #5781 http://dev.jquery.com/ticket/5781
			.css({ top: 0, left: 0 })
			.hide()
			.data("cg-menucombo");
			if (this.options.draggable) {
				this.menucombo.element.draggable({
					stop: function (event, ui) {
						self.pos = ui.position;
					}
				});
			}
			if ($.fn.bgiframe) {
				this.menucombo.element.bgiframe();
			}
			if (this.options.addClass != null) {
				this.menucombo.element.addClass(this.options.addClass);
			}
			if (this.options.addId != null) {
				this.menucombo.element.attr('id', this.options.addId);
			}
		},

		_delay: function (handler, delay) {
			function handlerProxy() {
				return (typeof handler === "string" ? instance[handler] : handler)
				.apply(instance, arguments);
			}
			var instance = this;
			return setTimeout(handlerProxy, delay || 0);
		},


		destroy: function () {
			this.element
			.removeClass("cg-autocomplete-input")
			.removeAttr("autocomplete")
			.removeAttr("role")
			.removeAttr("aria-autocomplete")
			.removeAttr("aria-haspopup");
			this.menucombo.element.remove();
			$.Widget.prototype.destroy.call(this);
		},

		_setOption: function (key, value) {
			$.Widget.prototype._setOption.apply(this, arguments);
			if (key === "source") {
				this._initSource();
			}
			if (key === "appendTo") {
				this.menucombo.element.appendTo($(value || "body", this.element[0].ownerDocument)[0]);
			}
			if (key === "disabled" && value && this.xhr) {
				this.xhr.abort();
			}
		},

		_initSource: function () {
			var self = this,
			array,
			url;

			if ($.isArray(this.options.source)) {
				array = this.options.source;
				this.source = function (request, response) {
					response($.cg.combogrid.filter(array, request.term));
				};
			} else if (typeof this.options.source === "string") {
				url = this.options.source;
				this.source = function (request, response) {
					if (self.xhr) {
						self.xhr.abort();
					}
					self.xhr = $.ajax({
						url: url,
						data: request,
						dataType: "json",
						success: function (data, status, xhr) {
							if (xhr === self.xhr) {
								response(data);
							}
							self.xhr = null;
						},
						error: function (xhr) {
							if (xhr === self.xhr) {
								response([]);
							}
							self.xhr = null;
						}
					});
				};
			} else {
				this.source = this.options.source;
			}
		},

		search: function (value, event) {
			value = value != null ? value : this.element.val();
			//reset pagination values on a new term search
			this.page = 1;
			this.rows = 20;
			// always save the actual value, not the one passed as an argument
			this.term = this.element.val();

			if (value.length < this.options.minLength) {
				return this.close(event);
			}

			clearTimeout(this.closing);
			if (this._trigger("search", event) === false) {
				return;
			}
			if (!this.options.searchButton) {
				return this._search(value);
			}
		},

		_search: function (value) {
			this.pending++;
			this.element.addClass("cg-loading");

			this.source({ term: value }, this.response);
		},

		_response: function (records, totalpage, content) {

			if (!this.options.disabled && content) {
				//content = this._normalize( content );
				this._suggest(records, totalpage, content);
				this._trigger("open");
			} else {
				this.close();
			}



			//            if (!this.options.disabled && content && content.length) {
			//                //content = this._normalize( content );
			//                this._suggest(records, totalpage, content);
			//                this._trigger("open");
			//            } else {
			//                this.close();
			//            }
			this.pending--;
			if (!this.pending) {
				this.element.removeClass("cg-loading");
			}
		},

		show: function () {
			var self = this;
			self.element.trigger('focus.combogrid');
			self.search();
			self.element.trigger('focus.combogrid');
			//self.element.focus();
			//            setTimeout(function () {
			//                //self._search(self.element.val());
			//                self._search();
			//            }, 1);
			//self.element.trigger('focus.combogrid');
		},

		dropdownClick: function () {
			var self = this;

			if (self.menucombo.element.is(":visible")) {
				clearTimeout(self.searching);
				self.close();
				//                setTimeout(function () {
				//                    self.close();
				//                    self.element.trigger('focus.combogrid');
				//                }, 350);
			}
			else {
				self.element.trigger('focus.combogrid');
				self.search();
				self.element.trigger('focus.combogrid');
			}
		},


		close: function (event) {
			var self = this;
			clearTimeout(this.closing);
			if (this.menucombo.element.is(":visible")) {
				this.menucombo.element.hide();
				this.menucombo.deactivate();
				$('.' + self.element.attr('id') + '.cg-keynav-next').unbind('click.combogrid');
				$('.' + self.element.attr('id') + '.cg-keynav-prev').unbind('click.combogrid');
				$('.' + self.element.attr('id') + '.cg-keynav-last').unbind('click.combogrid');
				$('.' + self.element.attr('id') + '.cg-keynav-first').unbind('click.combogrid');

				if (self.options.scrollBar) {
					$(this.menucombo.element).find(".cg-menu-item-wrapper").unbind('scroll');
				}

				if (!this.options.debug) this.menucombo.element.empty();
				this.options.sidx = self.options.sidx;
				this.cssCol = "";
				this.lastOrdered = "";
				this.options.rows = 20;
				if (!this.options.rememberDrag) {
					this.pos = null;
				}
				this._trigger("close", event);
			}
		},


		isOpened: function () {
			var self = this;
			return self.menucombo.element.is(":visible")
		},


		_change: function (event) {
			if (this.previous !== this.element.val()) {
				this._trigger("change", event, { item: this.selectedItem });
			}
		},

		_normalize: function (items) {
			// assume all items have the right format when the first item is complete
			if (items.length && items[0].label && items[0].value) {
				return items;
			}
			return $.map(items, function (item) {
				if (typeof item === "string") {
					return {
						label: item,
						value: item
					};
				}
				return $.extend({
					//label: item.label || item.value,
					//value: item.value || item.label
					value: $.parseJSON(item)
				}, item);
			});
		},

		_suggest: function (records, totalpage, items) {
			var self = this;
			var ul = this.menucombo.element
			.empty()
			.zIndex(this.element.zIndex() + 1);
			$('.' + self.element.attr('id') + '.cg-keynav-next').unbind('click.combogrid');
			$('.' + self.element.attr('id') + '.cg-keynav-prev').unbind('click.combogrid');
			$('.' + self.element.attr('id') + '.cg-keynav-last').unbind('click.combogrid');
			$('.' + self.element.attr('id') + '.cg-keynav-first').unbind('click.combogrid');
			$('.cg-colHeader-label').unbind('click.combogrid');
			this._renderHeader(ul, this.options.colModel);
			this._renderMenu(ul, items, this.options.colModel);
			//		var ul2 = $("<ul class='cg-menu'></ul>");
			//		this._renderMenu( ul2, items, this.options.colModel );
			//		ul2.appendTo(ul);


			if (this.options.scrollBar) {
				$(ul).find(".cg-menu-item-wrapper").unbind('scroll');

				//wraperDiv = $('<div class="cg-menu-item-wrapper" style="width:auto;height:' + this.options.scrollHeight + 'px;overflow:auto;" />');
				wraperDiv = $('<div class="cg-menu-item-wrapper" style="height:' + this.options.scrollHeight + 'px;" />');
				//cg-menu-item
				$(ul).find('.cg-comboItemRow').wrapAll(wraperDiv);

				$(ul).find(".cg-menu-item-wrapper").scroll(function (e) {
					self.element.trigger('focus.combogrid');
					//                    var isActive = self.element[0] === this.document[0].activeElement;
					//                    if (!isActive) {
					//                        //this.element.focus();
					//                        //self.element.focus();
					//                        self.element[0].focus();
					//                    }
				});

				$(wraperDiv).removeClass('cg-menu-item');
				if (this.options.showPager == false) {
					$(wraperDiv).removeClass('ui-corner-bottom');
				}
			}


			if (this.options.showPager) {
				this._renderPager(ul, records, totalpage);
			}


			// TODO refresh should check if the active item is still in the dom, removing the need for a manual deactivate
			this.menucombo.deactivate();
			this.menucombo.refresh();

			// size and position menu
			ul.show();
			this._resizeMenu();
			if (this.pos == null) {
				ul.position($.extend({
					of: this.element
				}, this.options.position));
			}
			if (this.options.autoFocus) {
				this.menucombo.next(new $.Event("mouseover"));
			}
		},

		_resizeMenu: function () {
			var ul = this.menucombo.element;
			if (this.options.width != null) {
				ul.css('width', this.options.width);
			} else {
				//ul.css('width', 'auto');                
				/*alert(Math.max(
				ul.width( "" ).outerWidth(),
				this.element.outerWidth()
				));
				ul.outerWidth( Math.max(
				ul.width( "" ).outerWidth(),
				this.element.outerWidth()
				));*/
			}
		},
		_renderHeader: function (ul, colModel) {
			var self = this;

			//            //title
			//            divTitle = $('<div class="cg-divTitle ui-state-default ui-corner-top">');
			//            divTitle.append('</div').appendTo(ul);


			div = $('<div id="cg-divHeader" class="ui-state-default ui-corner-top">');
			$.each(colModel, function (index, col) {
				if (col.width == undefined) { col.width = 100 / colModel.length; }
				if (col.align == undefined) { col.align = "center"; }
				var hide = "";
				if (col.hidden != undefined && col.hidden) {
					hide = "display:none;";
					if (col.width != undefined) col.width = 0;
				}
				// Check if column is ordered or not to provide asc/desc icon
				if (col.columnName == self.cssCol) {
					div.append('<div class="cg-colHeader" style="width:' + col.width + self.options.munit + ';' + hide + ' text-align:' + col.align + '"><label class="cg-colHeader-label" id="' + col.columnName + '">'
						+ self._renderLabel(col.label)
						+ '</label><span class="cg-colHeader ' + self.options.sord + '"></span></div>');
				} else {
					div.append('<div class="cg-colHeader" style="width:' + col.width + self.options.munit + ';' + hide + ' text-align:' + col.align + '"><label class="cg-colHeader-label" id="' + col.columnName + '">'
						+ self._renderLabel(col.label)
						+ '</label></div>');
				}
			});
			div.append('</div').appendTo(ul);
			if (this.options.draggable) {
				$('#cg-divHeader').css("cursor", "move");
			}
			$(".cg-colHeader-label").bind('click.combogrid', function () {
				self.options.sord = "";
				self.cssCol = "";
				value = $(this).attr('id');
				self.cssCol = value;
				if (self.lastOrdered == value) {
					self.lastOrdered = "";
					self.options.sord = "desc";
				} else {
					self.lastOrdered = value;
					self.options.sord = "asc";
				}
				self.options.sidx = value;
				self._search(self.term);
			});
		},
		_renderLabel: function (label) {
			if (this.options.i18n) {
				return $.i18n.prop(label);
			} else {
				return label;
			}
		},
		_renderMenu: function (ul, items, colModel) {
			var self = this;

			var totRecord = items.length;
			//alert(totRecord);

			if (totRecord > 0) {
				$.each(items, function (index, item) {
					self._renderItem(ul, item, colModel);
				});
			}
			else {
				//self._renderItemNoRecord(ul);
				if (self.options.showError) {
					self._renderItem(ul, null, colModel, self.errorNo, self.errorString);
				}
			}
		},
		_renderItem: function (ul, item, colModel, errorNo, errorString) {
			var self = this;

			errorNo = errorNo || 0;
			errorString = errorString || "No Record Found OR Error Occured!";

			this.rowNumber++;
			div = $("<div class='cg-colItem'>");

			if (errorNo > 0) {
				$("<div style='' class='cg-DivItem'>" + errorString + "</div>").appendTo(div);
			}
			else {
				$.each(colModel, function (index, col) {
					if (col.width == undefined) { col.width = 100 / colModel.length; }
					if (col.align == undefined) { col.align = "center"; }
					var hide = "";
					if (col.hidden != undefined && col.hidden) {
						hide = "display:none;";
					}
					var colItem;
					if (item[col.columnName] != null && typeof item[col.columnName] === "object") {
						subItem = item[col.columnName];
						colItem = subItem[col.subName]
					} else if (item[col.columnName] == null && self.options.replaceNull) {
						colItem = "";
					} else {
						colItem = item[col.columnName];
					}

					if (col.highlight == undefined) { col.highlight = 0; }
					if (typeof colItem === 'string') {
						var term = $.cg.combogrid.escapeRegex(self.dataterm);
						switch (col.highlight) {
							case 0:
								break;
							case 1: //exact
								break;
							case 2:   //starts with
								//var s1 = new RegExp("(^" + term + ")", "gi");
								var s1 = new RegExp("(^" + term + ")", "i");
								colItem = colItem.replace(s1, "<b>$1</b>");
								break;
							case 3: //endswith
								var e = new RegExp("(" + term + "$)", "i");
								colItem = colItem.replace(e, "<b>$1</b>");
								break;
							case 4: //contains
								var re = new RegExp("(" + term + ")", "i");
								colItem = colItem.replace(re, "<b>$1</b>");
								break;
						}
					}
					$("<div style='width:" + col.width + self.options.munit + ";" + hide + " text-align:" + col.align + "' class='cg-DivItem'>" + colItem + "</div>").appendTo(div);
				});
			}
			div.append("</div>");
			if (self.options.alternate) {
				if (this.rowNumber % 2 == 0) {
					return $("<div class='cg-comboItem-even cg-comboItemRow'></div>").data("item.combogrid", item).append(div).appendTo(ul);
				} else {
					return $("<div class='cg-comboItem-odd cg-comboItemRow'></div>").data("item.combogrid", item).append(div).appendTo(ul);
				}
			} else {
				return $("<div class='cg-comboItem cg-comboItemRow'></div>").data("item.combogrid", item).append(div).appendTo(ul);
			}
		},

		_renderItemShowString: function (ul, strShow) {
			var self = this;

			strShow = strShow || "No Record Found OR Error Occured!";

			this.rowNumber++;
			div = $("<div class='cg-colItem'>");

			$("<div style='' class='cg-DivItem'>" + strShow + "</div>").appendTo(div);

			div.append("</div>");
			//return $("<div class='cg-comboItem cg-comboItemRow'></div>").data("item.combogrid", item).append(div).appendTo(ul);
			return $("<div class='cg-comboItem-even cg-comboItemRow'></div>").data("item.combogrid", item).append(div).appendTo(ul);
		},

		_renderPager: function (ul, records, totalpage) {
			var self = this;
			var initRecord = ((self.page * self.options.rows) - self.options.rows) + 1;
			var lastRecord = 0;
			if (self.page < totalpage) {
				lastRecord = (self.page * self.options.rows);
			} else {
				lastRecord = records;
			}
			div = $("<div class='cg-comboButton ui-state-default ui-corner-bottom'>");
			$("<table cellspacing='0' cellpadding='0' border='0' class='cg-navTable'>"
			+ "<tbody>"
				+ "<td align='center' style='white-space: pre; width: 264px;' id='cg-keynav-center'>"
				+ "<table cellspacing='0' cellpadding='0' border='0' class='cg-pg-table' style='table-layout: auto;'>"
				+ "<tbody>"
					+ "<tr>"
						+ "<td class='cg-pg-button ui-corner-all cg-state-disabled cg-keynav-first " + self.element.attr('id') + "'>"
							+ "<span class='ui-icon ui-icon-seek-first'></span>"
						+ "</td>"
						+ "<td class='cg-pg-button ui-corner-all cg-state-disabled cg-keynav-prev " + self.element.attr('id') + "'>"
							+ "<span class='ui-icon ui-icon-seek-prev'></span>"
						+ "</td>"
						+ "<td style='width: 4px;' class='cg-state-disabled'>"
							+ "<span class='ui-separator'></span>"
						+ "</td>"
						+ "<td dir='ltr' id='cg-navInfo'>"
						+ self._renderPagerPage('page', self.page, totalpage)
						+ "</td>"
						+ "<td style='width: 4px;' class='cg-state-disabled'>"
							+ "<span class='ui-separator'></span>"
						+ "</td>"
						+ "<td class='cg-pg-button ui-corner-all cg-keynav-next " + self.element.attr('id') + "'>"
							+ "<span class='ui-icon ui-icon-seek-next'></span>"
						+ "</td>"
						+ "<td class='cg-pg-button ui-corner-all cg-keynav-last " + self.element.attr('id') + "'>"
							+ "<span class='ui-icon ui-icon-seek-end'></span>"
						+ "</td>"
			// Select page
						+ "<td dir='ltr'>"
							+ "<select class='" + self.element.attr('id') + " recordXP'>"
							+ "</select>"
						+ "</td>"
					+ "</tr>"
				+ "</tbody>"
				+ "</table>"
				+ "</td>"
				+ "<td align='right' id='cg-keynav-right'>"
					+ "<div class='ui-paging-info' style='text-align: right;' dir='ltr'>"
					+ self._renderPagerView('recordtext', initRecord, lastRecord, records)
					+ "</div>"
				+ "</td>"
			+ "</tr>"
		+ "</tbody>"
		+ "</table>").appendTo(div);
			div.append("</div>");
			div.appendTo(ul);
			$.each(self.options.rowsArray, function (index, value) {
				optVal = value == 'all' ? 0 : value;
				//$('.' + self.element.attr('id') + '.recordXP').append("<option value='" + value + "' role='option'>" + value + "</option>");
				$('.' + self.element.attr('id') + '.recordXP').append("<option value='" + optVal + "' role='option'>" + value + "</option>");
			});
			$('.' + self.element.attr('id') + '.recordXP').val(self.options.rows);
			if (self.page > 1) {
				$('.' + self.element.attr('id') + '.cg-keynav-first').removeClass("cg-state-disabled");
				$('.' + self.element.attr('id') + '.cg-keynav-prev').removeClass("cg-state-disabled");
			} else {
				$('.' + self.element.attr('id') + '.cg-keynav-first').addClass("cg-state-disabled");
				$('.' + self.element.attr('id') + '.cg-keynav-prev').addClass("cg-state-disabled");
			};
			if (self.page == totalpage) {
				$('.' + self.element.attr('id') + '.cg-keynav-next').addClass("cg-state-disabled");
				$('.' + self.element.attr('id') + '.cg-keynav-last').addClass("cg-state-disabled");
			};

			$('.' + self.element.attr('id') + '.cg-keynav-next').bind('click.combogrid', function () {
				if (self.page < totalpage) {
					self.page++;
					self._search(self.term);
					self.element.trigger('focus.combogrid');
				}
			});
			$('.' + self.element.attr('id') + '.cg-keynav-prev').bind('click.combogrid', function () {
				if (self.page > 1) {
					self.page--;
					self._search(self.term);
					self.element.trigger('focus.combogrid');
				}
			});
			$('.' + self.element.attr('id') + '.cg-keynav-last').bind('click.combogrid', function () {
				if (total > 1 && self.page < totalpage) {
					self.page = totalpage;
					self._search(self.term);
					self.element.trigger('focus.combogrid');
				}
			});
			$('.' + self.element.attr('id') + '.cg-keynav-first').bind('click.combogrid', function () {
				if (total > 1 && self.page > 1) {
					self.page = 1;
					self._search(self.term);
					self.element.trigger('focus.combogrid');
				}
			});
			//            $('.' + self.element.attr('id') + '.currentPage').keypress(function (e) {
			//                var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
			//                if (key == 13) {
			//                    if (!isNaN($(this).val()) && $(this).val() != 0) {
			//                        if ($(this).val() > total) {
			//                            self.page = total;
			//                        } else {
			//                            self.page = $(this).val();
			//                        }
			//                        self._search(self.term);
			//                    }
			//                }
			//            });

			$('.' + self.element.attr('id') + '.currentPage').keydown(function (e) {
				var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
				if (key == 13) {
					if (!isNaN($(this).val()) && $(this).val() != 0) {
						if ($(this).val() > totalpage) {
							self.page = totalpage;
						} else {
							self.page = $(this).val();
						}
						self._search(self.term);
					}
					e.preventDefault();
					self.element.trigger('focus.combogrid');
				}
			});


			$('.' + self.element.attr('id') + '.recordXP').bind('change', function () {
				self.options.rows = this.value;
				self.page = 1;
				self._search(self.term);
				self.element.trigger('focus.combogrid');
			});
			return div;
		},
		_renderPagerPage: function (label, page, totalpage) {
			var self = this;
			if (this.options.i18n) {
				return $.i18n.prop('page') + ' <input type="text" size="1" class="' + self.element.attr('id') + ' currentPage cg-pg-CurrentPage" value="' + page + '"></input> ' + $.i18n.prop('of') + ' ' + totalpage;
			} else {
				return 'Page <input type="text" size="1" class="' + self.element.attr('id') + ' currentPage cg-pg-CurrentPage" value="' + page + '"></input> of ' + totalpage;
			}
		},
		_renderPagerView: function (label, initRecord, lastRecord, records) {
			var self = this;
			if (this.options.i18n) {
				return $.i18n.prop(label, initRecord, lastRecord, records);
			} else {

				if (records == 0) {
					return "View 0 - 0 of 0";
				}
				else {
					return "View " + initRecord + " - " + lastRecord + " of " + records;
				}

//                if (self.actualRecords == 0) {
//                    return "View 0 - 0 of 0";
//                }
//                else {
//                    return "View " + initRecord + " - " + lastRecord + " of " + records;
//                }
			}
		},
		_move: function (direction, event) {
			if (!this.menucombo.element.is(":visible")) {
				this.search(null, event);
				return;
			}
			if (this.menucombo.first() && /^previous/.test(direction) ||
				this.menucombo.last() && /^next/.test(direction)) {
				this.element.val(this.term);
				this.menucombo.deactivate();
				return;
			}
			this.menucombo[direction](event);
		},

		widget: function () {
			return this.menucombo.element;
		}
	});

	$.extend($.cg.combogrid, {
		escapeRegex: function (value) {
			return value.replace(/[-[\]{}()*+?.,\\^$|#\s]/g, "\\$&");
		},
		filter: function (array, term) {
			var matcher = new RegExp($.cg.combogrid.escapeRegex(term), "i");
			return $.grep(array, function (value) {
				return matcher.test(value.label || value.value || value);
			});
		}
	});

} (jQuery));

/*
 * jQuery UI Menu (not officially released)
 * 
 * This widget isn't yet finished and the API is subject to change. We plan to finish
 * it for the next release. You're welcome to give it a try anyway and give us feedback,
 * as long as you're okay with migrating your code later on. We can help with that, too.
 *
 * Copyright 2010, AUTHORS.txt (http://jqueryui.com/about)
 * Dual licensed under the MIT or GPL Version 2 licenses.
 * http://jquery.org/license
 *
 * http://docs.jquery.com/UI/Menu
 *
 * Depends:
 *	jquery.ui.core.js
 *  jquery.ui.widget.js
 */
(function($) {

$.widget("cg.menucombo", {
	_create: function() {
		var self = this;
		this.element
			.addClass("cg-menu ui-widget ui-widget-content ui-corner-all combogrid")
			.attr({
				role: "listbox",
				"aria-activedescendant": "ui-active-menuitem"
			})
			.click(function( event ) {
//				if ( !$( event.target ).closest( ".cg-menu-item div" ).length ) {
//					return;
//				}
//				// temporary
//				event.preventDefault();
//				self.select( event );
			});
		this.refresh();
	},
	
	refresh: function() {
		var self = this;
		// don't refresh list items that are already adapted
//		var items = this.element.children("div:not(.cg-menu-item):not(#cg-divHeader):not(.cg-comboButton):has(div)")
//			.addClass("cg-menu-item")
//			.attr("role", "menuitem");

	   
		var items = this.element.find(".cg-comboItemRow")
			.addClass("cg-menu-item")
			.attr("role", "menuitem");
		
		items.children("div")
			.addClass("ui-corner-all")
			.attr("tabindex", -1)
			// mouseenter doesn't work with event delegation
			.mouseenter(function( event ) {
				self.activate( event, $(this).parent() );
			})
			.mouseleave(function() {
				//self.deactivate();
			});

//       items.children("div")
//        	.addClass("ui-corner-all")
//			.attr("tabindex", -1);


			items.mouseenter(function( event ) {
				self.activate( event, $(this));
			})
			.mouseleave(function() {
				self.deactivate();
			});

			items.click(function(event){
				 event.preventDefault();
				 self._trigger("selected", event, { item: self.active });
			});

	},

	activate: function( event, item ) {
		this.deactivate();
		if (this.hasScroll()) {
			var offset = item.offset().top - this.element.offset().top,
				scroll = this.element.attr("scrollTop"),
				elementHeight = this.element.height();
			if (offset < 0) {
				this.element.attr("scrollTop", scroll + offset);
			} else if (offset >= elementHeight) {
				this.element.attr("scrollTop", scroll + offset - elementHeight + item.height());
			}
		}

		if (this.options.scrollBar && item!=null)
		{
		   wraperDiv= this.element.find('.cg-menu-item-wrapper');
		   if ($(item).is(':visible'))
		   {
				itemTop = item.offset().top;
				wraperHeight = wraperDiv.height();
				itemHeight = item.height();
				viewStart = wraperDiv.offset().top;
				viewEnd = viewStart + wraperHeight;
				
				wraperScrollTop = $(wraperDiv).scrollTop();
				inView = this._isItemInView(item, wraperDiv);
				//var offset = itemTop - viewEnd;
				var offset = viewEnd - itemTop;
				scrollValue = 0;
			   if (this._isItemInView(item, wraperDiv)==false)
			   {
				   if (offset >= 0){
						//scrollValue = offset + wraperScrollTop + itemHeight + 4;                         
						 if (offset > wraperHeight) {
							 scrollValue = wraperScrollTop - (offset - wraperHeight);
						 }
						 else {
						
							scrollValue = itemHeight - offset + wraperScrollTop + 4;
						 }
				   }
				   else {
						 scrollValue = itemHeight - offset + wraperScrollTop + 4;
				   }
				  //$(wraperDiv).scrollTop(scrollValue);
				  $(wraperDiv).animate({ scrollTop: scrollValue },100);
				}
				 //$('#Text1').val('iTop:' + itemTop + ', vEnd:' + viewEnd + ', off:' + offset + ', sTop:' + wraperScrollTop + ',newScroll:' + scrollValue);  
		  }
		}

		
		this.active = item.eq(0)
				//.children("div")
				.addClass("ui-state-hover")
				.attr("id", "ui-active-menuitem")
			.end();
		this._trigger("focus", event, { item: item });

	  
	},

	deactivate: function() {
		if (!this.active) { return; }
		this.active
		 //.children("div")
			.removeClass("ui-state-hover")
			.removeAttr("id");
		this._trigger("blur");
		this.active = null;
	},

	_isItemInView: function(elem,container){
			//var docViewTop = $(container).scrollTop();
			
			if (elem == null | container == null)
			{
			  return false;
			}

			if ($(container).is(':visible') == false | $(elem).is(':visible') == false)
			{
			   return false;
			}
			var docViewTop =  $(container).offset().top;
			var docViewBottom = docViewTop + $(container).height();

			var elemTop = $(elem).offset().top;
			var elemBottom = elemTop + $(elem).height();

			return ((elemBottom <= docViewBottom) && (elemTop >= docViewTop));

	},

	next: function(event) {
		this.move("next", ".cg-menu-item:first", event);
	},

	previous: function(event) {
		this.move("prev", ".cg-menu-item:last", event);
	},

	first: function() {
		return this.active && !this.active.prevAll(".cg-menu-item").length;
	},

	last: function() {
		return this.active && !this.active.nextAll(".cg-menu-item").length;
	},

	move: function(direction, edge, event) {
		if (!this.active) {
			//this.activate(event, this.element.children(edge));
			this.activate(event, this.element.find(edge));
			return;
		}
		var next = this.active[direction + "All"](".cg-menu-item").eq(0);
		if (next.length) {
			this.activate(event, next);
		} else {
			//this.activate(event, this.element.children(edge));
			this.activate(event, this.element.find(edge));
		}
	},

	// TODO merge with previousPage
	nextPage: function(event) {
		if (this.hasScroll()) {
			// TODO merge with no-scroll-else
			if (!this.active || this.last()) {
				//this.activate(event, this.element.children(".cg-menu-item:first"));
				this.activate(event, this.element.find(".cg-menu-item:first"));
				return;
			}
			var base = this.active.offset().top,
				height = this.element.height(),
				
//                result = this.element.children(".cg-menu-item").filter(function() {
//					var close = $(this).offset().top - base - height + $(this).height();
//					// TODO improve approximation
//					return close < 10 && close > -10;
//				});

				result = this.element.find(".cg-menu-item").filter(function() {
					var close = $(this).offset().top - base - height + $(this).height();
					// TODO improve approximation
					return close < 10 && close > -10;
				});

			// TODO try to catch this earlier when scrollTop indicates the last page anyway
			if (!result.length) {
				//result = this.element.children(".cg-menu-item:last");
				result = this.element.find(".cg-menu-item:last");
			}
			this.activate(event, result);
		} else {
//			this.activate(event, this.element.children(".cg-menu-item")
//				.filter(!this.active || this.last() ? ":first" : ":last"));
		   this.activate(event, this.element.find(".cg-menu-item")
				.filter(!this.active || this.last() ? ":first" : ":last"));
		}
	},

	// TODO merge with nextPage
	previousPage: function(event) {
		if (this.hasScroll()) {
			// TODO merge with no-scroll-else
			if (!this.active || this.first()) {
				//this.activate(event, this.element.children(".cg-menu-item:last"));
				this.activate(event, this.element.find(".cg-menu-item:last"));
				return;
			}

			var base = this.active.offset().top,
				height = this.element.height();
//				result = this.element.children(".cg-menu-item").filter(function() {
//					var close = $(this).offset().top - base + height - $(this).height();
//					// TODO improve approximation
//					return close < 10 && close > -10;
//				});
				result = this.element.find(".cg-menu-item").filter(function() {
					var close = $(this).offset().top - base + height - $(this).height();
					// TODO improve approximation
					return close < 10 && close > -10;
				});


			// TODO try to catch this earlier when scrollTop indicates the last page anyway
			if (!result.length) {
				//result = this.element.children(".cg-menu-item:first");
				result = this.element.find(".cg-menu-item:first");
			}
			this.activate(event, result);
		} else {
			this.activate(event, this.element.find(".cg-menu-item")
				.filter(!this.active || this.first() ? ":last" : ":first"))
//			this.activate(event, this.element.children(".cg-menu-item")
//				.filter(!this.active || this.first() ? ":last" : ":first"));
		}
	},

	hasScroll: function() {
		return this.element.height() < this.element.attr("scrollHeight");
	},

	select: function( event ) {
		this._trigger("selected", event, { item: this.active });
	}
});

}(jQuery));


